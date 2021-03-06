﻿using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WasselApp.Models;
using WasselApp.Helpers;
using WasselApp.Views.HomeMaster;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Multilingual;

namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrickOrderDetailsPage : ContentPage
    {
        public BrickOrderDetailsPage(Car carOrder)
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            Settings.LastUsedDriverID = carOrder.Member.id;
            if (AddressTo.Text != null || AddressFrom.Text != null)
            {
                Settings.Placeto = String.Empty;
                AddressTo.Text = null;
                AddressFrom.Text = null;
                GetLocation();
            }
            else
            {
                Settings.Placeto = String.Empty;
                AddressTo.Text = null;
                GetLocation();
            }
        }
        private async void GetLocation()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus == PermissionStatus.Granted)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);
                var addresses = await Geocoding.GetPlacemarksAsync(location);
                var placemark = addresses?.FirstOrDefault();
                if (placemark != null)
                {
                    if (addresses.FirstOrDefault().Thoroughfare != null)
                    {
                        AddressFrom.Text = placemark.Thoroughfare + " , " + placemark.AdminArea + " , " + placemark.CountryName;
                        Settings.PlaceFrom = AddressFrom.Text;
                    }
                    else
                    {
                        AddressFrom.Text = AppResources.LocationNotFound;

                    }
                }
                else
                {
                    AddressFrom.Text = AppResources.LocationNotFound;
                }
                Settings.LastLat = Settings.Latfrom = location.Latitude.ToString();
                Settings.LastLng = Settings.Lngfrom = location.Longitude.ToString();
            }
            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionLocationDetails,
                    AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }

        }

        private async void LocationTobtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlaceToPage(), true);

        }
        protected override void OnAppearing()
        {
            AddressTo.Text = Settings.Placeto;
            AddressFrom.Text = Settings.PlaceFrom;
            CalculatDistance();
            base.OnAppearing();
        }
        //private async void OnNotificationOpened(OSNotificationOpenedResult result)
        //{
        //    if (result.notification?.payload?.additionalData == null)
        //    {
        //        return;
        //    }

        //    if (result.notification.payload.additionalData.ContainsKey("body"))
        //    {
        //        var labelText = result.notification.payload.additionalData["body"].ToString();
        //        Settings.LastNotify = labelText;
        //        CheckNotification();
        //    }

        //}

        //private async void OnNotificationRecevied(OSNotification notification)
        //{

        //    if (notification.payload?.additionalData == null)
        //    {
        //        return;
        //    }

        //    if (notification.payload.additionalData.ContainsKey("body"))
        //    {
        //        var labelText = notification.payload.additionalData["body"].ToString();
        //        Settings.LastNotify = labelText;
        //        CheckNotification();
        //    }
        //}

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
            Settings.Distance = Location.CalculateDistance(FromLocation, ToLocation, (DistanceUnits.Kilometers));
            distancelbl.Text = Convert.ToInt32(Settings.Distance).ToString();
        }

        private void DatePickerShow(object sender, EventArgs e)
        {
            datepicker.Focus();
        }

        private async void Confirmbtn_Clicked(object sender, EventArgs e)
        {
            Active.IsRunning = true;
            var date = String.Format("{0:yyyy-MM-dd}", datepicker.Date);
            //   CalculatDistance();
            CarOrder order = new CarOrder
            {
                latfrom = Settings.Latfrom,
                lngfrom = Settings.Lngfrom,
                user_id = Settings.LastUsedDriverID,
                owner_id = Settings.LastUsedID,
                addressto = Settings.Placeto,
                addressfrom = Settings.PlaceFrom,
                ordertype = "نقل أجره",
           //     weight = int.Parse(Weightentry.Text),
                // car_model_id = Settings.LastUsedCarModel.ToString(),
                distance = Convert.ToInt32(Settings.Distance),
                latto = Settings.Latto,
                lngto = Settings.Lngto,
                created_at = datepicker.Date.ToString(),
            };
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("user_id", order.user_id.ToString());
            values.Add("owner_id", order.owner_id.ToString());
            values.Add("latto", order.latto);
            values.Add("lngto", order.lngto);
            values.Add("latfrom", order.latfrom);
            values.Add("lngfrom", order.lngfrom);
            values.Add("addressto", order.addressto);
            values.Add("addressfrom", order.addressfrom);
            values.Add("ordertype", order.ordertype);
         //   values.Add("weight", order.weight.ToString());
            values.Add("distance", order.distance.ToString());
            //values.Add("car_model", order.car_model_id);
            // values.Add("waiting_hours", order.created_at);
            string content = JsonConvert.SerializeObject(values);
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.PostAsync("https://waselksa.alsalil.net/api/addograorder",
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
                    Settings.CarOrderid = json.data.id;
                    ShowPanel.IsVisible = false;
                    Confirmbtn.IsVisible = false;
                    Active.IsRunning = true;
                }
            }
            catch (Exception)
            {
                Active.IsRunning = false;
                await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (NoteEditor.IsVisible != true)
            {
                NoteEditor.IsVisible = true;
            }
            else
            {
                NoteEditor.IsVisible = false;
            }
        }
        private void Datepicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private async void UserPlacebtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserPlacePage(), true);
        }
    }
}