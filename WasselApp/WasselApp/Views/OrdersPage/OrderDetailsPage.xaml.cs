using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using WasselApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using WasselApp.Views.HomeMaster;

namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsPage : ContentPage
    {
        public OrderDetailsPage(Car carOrder)
        {
            InitializeComponent();
        }
        PlaceToPage toPage;
        private string locationto;
        private async void LocationTobtn_Clicked(object sender, EventArgs e)
        {
            toPage = new PlaceToPage(locationto);
            toPage.btn.Clicked += Btn_Clicked;
            await Navigation.PushAsync(toPage,true);
        }
        private async void Btn_Clicked(object sender, EventArgs e)
        {
          //  Settings.Placeto = String.Empty;
          //  Settings.Placeto = toPage.location;
            AddressTo.Text = Settings.Placeto;
            await Navigation.PopAsync();
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
                CheckNotification();
            }

        }

        private async void OnNotificationRecevied(OSNotification notification)
        {

            if (notification.payload?.additionalData == null)
            {
                return;
            }

            if (notification.payload.additionalData.ContainsKey("body"))
            {
                var labelText = notification.payload.additionalData["body"].ToString();
                Settings.LastNotify = labelText;
                CheckNotification();
            }
        }

        private async void CheckNotification()
        {
            if (Settings.LastNotify == "Has been approved")
            {
                Active.IsRunning = false;
                App.Current.MainPage = new OrderSuccessPage();
                await DisplayAlert(AppResources.Alert, AppResources.OrderRefused, AppResources.Ok);
                //Device.BeginInvokeOnMainThread(() => {
                //    Navigation.PushModalAsync(new TirhalPage());
                //     DisplayAlert(AppResources.Alert, AppResources.OrderAccepted, AppResources.OK);

                //});
            }
            else
            {
                Active.IsRunning = false;
                App.Current.MainPage = new MainPage();
                await DisplayAlert(AppResources.Alert, AppResources.OrderRefused, AppResources.Ok);
                //Device.BeginInvokeOnMainThread(() => {
                //    Navigation.PushModalAsync(new CarTypePage());
                //     DisplayAlert(AppResources.Alert, AppResources.OrderRefused, AppResources.OK);
                //});

            }
        }

       
        public void CalculatDistance()
        {
            Location FromLocation = new Location(Convert.ToDouble(Settings.Latfrom), Convert.ToDouble(Settings.Lngfrom));
            Location ToLocation = new Location(Convert.ToDouble(Settings.Latto), Convert.ToDouble(Settings.Lngto));
            double Distance = Location.CalculateDistance(FromLocation, ToLocation, (DistanceUnits.Kilometers));
        }

        private void DatePickerShow(object sender, EventArgs e)
        {
            datepicker.Focus();
        }

        //private async void Confirmbtn_Clicked(object sender, EventArgs e)
        //{
        //    Active.IsRunning = true;
        //    var date = String.Format("{0:yyyy-MM-dd}", datepicker.Date);
        //    TirhalOrder order = new TirhalOrder
        //    {
        //        latfrom = Settings.Latfrom,
        //        lngfrom = Settings.Lngfrom,
        //        user_id = Settings.LastUsedID.ToString(),
        //        driver_id = Settings.LastUsedDriverID.ToString(),
        //        car_model_id = Settings.LastUsedCarModel.ToString(),

        //        latto = Settings.Latto,
        //        lngto = Settings.Lngto,
        //        created_at = datepicker.Date.ToString(),

        //    };
        //    Dictionary<string, string> values = new Dictionary<string, string>();
        //    values.Add("user_id", order.user_id.ToString());
        //    values.Add("driver_id", order.driver_id);
        //    values.Add("latfrom", order.latfrom);
        //    values.Add("lngfrom", order.lngfrom);
        //    values.Add("done", order.done.ToString());
        //    values.Add("latto", order.latto);
        //    values.Add("lngto", order.lngto);
        //    values.Add("car_model", order.car_model_id);
        //    values.Add("created_at", order.created_at);
        //    string content = JsonConvert.SerializeObject(values);
        //    var httpClient = new HttpClient();
        //    try
        //    {
        //        var response = await httpClient.PostAsync("http://wassel.alsalil.net/api/addtirhalorder",
        //            new StringContent(content, Encoding.UTF8, "text/json"));
        //        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
        //        var json = JsonConvert.DeserializeObject<Response<TirhalOrder, string>>(serverResponse);
        //        if (json.success == false)
        //        {
        //            Active.IsRunning = false;
        //            await DisplayAlert(AppResources.Error, json.message, AppResources.OK);
        //        }
        //        else
        //        {
        //            Active.IsRunning = false;
        //            await DisplayAlert(AppResources.OrderSuccess, json.message, AppResources.OK);
        //            Settings.carorderid = json.data.id;
        //            ShowPanel.IsVisible = false;
        //            Confirmbtn.IsVisible = false;
        //            Active.IsRunning = true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Active.IsRunning = false;
        //        await DisplayAlert(AppResources.ErrorMessage, AppResources.ErrorMessage, AppResources.OK);
        //    }

        //}

        private void Button_Clicked(object sender, EventArgs e)
        {
            NoteEditor.IsVisible = true;
        }

        

       

        private void Datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

       
    }
}
