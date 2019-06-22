using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using Plugin.Multilingual;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using WasselApp.Helpers;
using WasselApp.Models;
using WasselApp.Views.DriverPages.DriverOrders;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.DriverPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverMainPage : ContentPage
    {
        public DriverMainPage()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            GetLocation();
            CheckUserStatus();
           
        }
        private async void GetLocation()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                locationStatus = results[Permission.Location];
            }
            if (locationStatus == PermissionStatus.Granted)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMilliseconds(5000));
                    var location = await Geolocation.GetLocationAsync(request);
                    MainMap.MapRegion = MapSpan.FromCenterAndRadius(
                            new Position(location.Longitude, location.Longitude), Distance.FromKilometers(50));
                }
                catch (FeatureNotEnabledException)
                {
                    await DisplayAlert(AppResources.Alert, AppResources.LocationEnabled, AppResources.Ok);
                }
            }
            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionLocationDetails,
                    AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }
       
        private void CheckUserStatus()
        {
            if (Settings.LastUserStatus == "0")
            {
                UserStatuslbl.IsVisible = true;
            }
            else
            {
                UserStatuslbl.IsVisible = false;
            }

        }

        private async void UserLocationChanged(object sender, TK.CustomMap.TKGenericEventArgs<TK.CustomMap.Position> e)
        {
          
            var x = e.Value.Latitude;
            var y =e.Value.Longitude;
            if (Settings.CarLat != x || Settings.CarLng != y)
            {
                if (Settings.LastUserStatus != "0")
                {
                    Settings.CarLat = x;
                    Settings.CarLng = y;
                    try
                    {
                        var CurrentLocation = new Position(x, y);
                        if (CurrentLocation != null)
                        {
                            Dictionary<string, string> values = new Dictionary<string, string>();
                            values.Add("user_id", Settings.LastUsedDriverID.ToString());
                            values.Add("lat", Settings.LastLat);
                            values.Add("lng", Settings.LastLng);
                            string content = JsonConvert.SerializeObject(values);
                            var httpClient = new HttpClient();
                            try
                            {
                                var response = await httpClient.PostAsync("http://waselksa.alsalil.net/api/updatelocal",
                                    new StringContent(content, Encoding.UTF8, "text/json"));
                                var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                                var json = JsonConvert.DeserializeObject<Response<string, string>>(serverResponse);

                            }
                            catch (Exception)
                            {
                                await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }
                }

            }


        }

    }
}