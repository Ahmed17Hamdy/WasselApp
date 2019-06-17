using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using WasselApp.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPlacePage : ContentPage
    {
        public UserPlacePage()
        {
            InitializeComponent();
            _ = GetUserLocationAsync();
        }
        private async Task GetUserLocationAsync()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus != PermissionStatus.Granted)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMilliseconds(5000));
                var location = await Geolocation.GetLocationAsync(request);
                var addresses = await Geocoding.GetPlacemarksAsync(location);
                var placemark = addresses?.FirstOrDefault();
                if (placemark != null)
                {
                    if (addresses.FirstOrDefault().Thoroughfare != null)
                    {
                        addlbl.Text = placemark.Thoroughfare + " , " + placemark.AdminArea + " , " + placemark.CountryName;
                        Settings.PlaceFrom = addlbl.Text;
                    }
                    else
                    {
                        addlbl.Text = AppResources.LocationNotFound;

                    }
                }
                else
                {
                    addlbl.Text = AppResources.LocationNotFound;
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

        private async void Continuebtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void MainMap_UserLocationChanged(object sender, TK.CustomMap.TKGenericEventArgs<TK.CustomMap.Position> e)
        {
            Settings.LastLat = Settings.Latfrom =  String.Empty;
            Settings.LastLng = Settings.Lngfrom = String.Empty;
            Settings.PlaceFrom = String.Empty;
            var x = ToMap.MapCenter.Latitude;
            var y = ToMap.MapCenter.Longitude;
            var addresses = await Geocoding.GetPlacemarksAsync(x, y);
            var placemark = addresses?.FirstOrDefault();
            if (placemark != null)
            {
                if (addresses.FirstOrDefault().Thoroughfare != null)
                {
                    addlbl.Text = placemark.Thoroughfare + " , " + placemark.AdminArea + " , " + placemark.CountryName;
                    Settings.LastLat = Settings.Latfrom= x.ToString();
                    Settings.LastLng = Settings.Lngfrom = y.ToString();
                    Settings.PlaceFrom = addlbl.Text;
                    Continuebtn.IsVisible = true;
                }
                else
                {
                    addlbl.Text = AppResources.LocationNotFound;
                    Continuebtn.IsVisible = false;
                }
            }
            else
            {
                addlbl.Text = AppResources.LocationNotFound;
            }

        }

    }
}