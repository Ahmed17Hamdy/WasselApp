using Com.OneSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap.Api.Google;
using Xamarin.Forms;
using WasselApp.Helpers;
using Xamarin.Forms.Xaml;
using Com.OneSignal.Abstractions;
using TK.CustomMap;
using Xamarin.Essentials;
using TK.CustomMap.Overlays;
using Newtonsoft.Json;
using WasselApp.Models;
using System.Net.Http;
using Plugin.Multilingual;

namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSuccessPage : ContentPage
    {
        private string title;
        List<TKRoute> routes = new List<TKRoute>();
        List<TKCustomMapPin> Pins = new List<TKCustomMapPin>();
        public OrderSuccessPage()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                 : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            OneSignal.Current.StartInit("f5f4f650-3453-456c-8024-010ea68e738b")
          .InFocusDisplaying(OSInFocusDisplayOption.None)
          .HandleNotificationReceived(OnNotificationRecevied)
          .HandleNotificationOpened(OnNotificationOpened)
          .EndInit();
          GmsDirection.Init("AIzaSyB7rB6s8fc317zCPz8HS_yqwi7HjMsAqks");
            SetMyLocation();
        }
        private async void OrderMap_RouteCalculationFailed(object sender,
           TKGenericEventArgs<TK.CustomMap.Models.TKRouteCalculationError> e)
        {
            await DisplayAlert(AppResources.Error, AppResources.RouteNotFound, AppResources.Ok);

            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);
            OrderMap.MapRegion = MapSpan.FromCenterAndRadius(
                    new Position(location.Longitude, location.Longitude), Distance.FromMiles(1));
        }
        private void OrderMap_RouteCalculationFinished(object sender, TKGenericEventArgs<TKRoute> e)
        {
            OrderMap.MapRegion = e.Value.Bounds;
        }

        private async void OnNotificationOpened(OSNotificationOpenedResult result)
        {
            if (result.notification?.payload?.additionalData == null)
            {
                return;
            }

            if (result.notification.payload.additionalData.ContainsKey("body"))
            {
                var labelText = result.notification.payload.additionalData["body"].ToString();
                Settings.LastNotify = labelText;
                await DisplayAlert(AppResources.Error, Settings.LastNotify, AppResources.Ok);
            }

        }

        private void OnNotificationRecevied(OSNotification notification)
        {

            if (notification.payload?.additionalData == null)
            {
                return;
            }

            if (notification.payload.additionalData.ContainsKey("body"))
            {
                var title = notification.payload.additionalData["title"].ToString();
                if (title != "The trip is completed")
                {
                    var labelText = notification.payload.additionalData["body"].ToString();
                    var Req = JsonConvert.DeserializeObject<DelivaryObject>(labelText);
                    Settings.CarLat = Req.Position.Latitude;
                    Settings.CarLng = Req.Position.Longitude;
                    
                    SetMyLocation();
                }
                else
                {
                   App.Current.MainPage = new HomePage();
                    DisplayAlert(AppResources.Alert, AppResources.TripCompleted, AppResources.Ok);

                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    //    Navigation.PushModalAsync(new HomePage());
                    //});

                }

            }
        }
        private void SetMyLocation()
        {
            //    MoveMapToMyLocation();
            var route = new TKRoute();
            route.TravelMode = TKRouteTravelMode.Driving;
            var myposition = new Position(31.209523, 31.491892);
            var toposition = new Position(Convert.ToDouble(Settings.Latfrom), Convert.ToDouble(Settings.Lngfrom));
            route.Source = myposition;
            route.Destination = toposition;
            route.Color = Color.DodgerBlue;
            route.LineWidth = 25;
            Pins.Add(new RoutePin
            {
                Route = route,
                IsSource = true,
                IsDraggable = false,
                Position = myposition,
                Title = "From",
                ShowCallout = true,
                DefaultPinColor = Color.Green
            });
            Pins.Add(new RoutePin
            {
                Route = route,
                IsSource = true,
                IsDraggable = false,
                Position = toposition,
                Title = "To",
                ShowCallout = true,
                DefaultPinColor = Color.Red
            });
            routes.Add(route);
            OrderMap.Routes = routes;
            OrderMap.Pins = Pins;
        }
        private async void Cancelbtn_Clicked(object sender, EventArgs e)
        {
            CarOrder order = new CarOrder
            {
                owner_id = Settings.LastUsedID,
                id = Settings.LastOrderid,
                user_id = Settings.LastUsedDriverID
            };
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("user_id", order.user_id.ToString());
            values.Add("order_id", order.id.ToString());
            values.Add("owner_id", order.owner_id.ToString());
            string content = JsonConvert.SerializeObject(values);
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsync("http://waselksa.alsalil.net/api/usercancelwaselorder",
                    new StringContent(content, Encoding.UTF8, "text/json"));
                var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                var json = JsonConvert.DeserializeObject<Response<CarOrder, string>>(serverResponse);
                if (json.success == false)
                {
                          Active.IsRunning = false;
                    await DisplayAlert(AppResources.Error, json.message, AppResources.Ok);
                }
                else
                {
                          Active.IsRunning = false;
                    await DisplayAlert(AppResources.OrderSuccess, json.message, AppResources.Ok);
                    Device.BeginInvokeOnMainThread(() => {
                        Navigation.PushModalAsync(new HomePage());
                    });
                    //   Settings.carorderid = json.data.id;
                }
            }
            catch (Exception)
            {
                //   Active.IsRunning = false;
                await DisplayAlert(AppResources.ErrorMessage, AppResources.ErrorMessage, AppResources.Ok);
            }
        }
    }
}