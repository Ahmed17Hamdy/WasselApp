using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using WasselApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions.Abstractions;
using WasselApp.Models;
using Plugin.Permissions;
using TK.CustomMap;

namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPinOnMapPage : ContentPage
    {
        public AddPinOnMapPage()
        {
            InitializeComponent();
            _ = GetUserLocationAsync();
        }
        private async Task GetUserLocationAsync()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                locationStatus = results[Permission.Location];
            }
            if (locationStatus == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    ToMap.MoveToMapRegion(MapSpan.FromCenterAndRadius(
                   new Position(location.Longitude, location.Longitude), Distance.FromKilometers(50)), true);
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
        private async void MainMap_UserLocationChanged(object sender, TK.CustomMap.TKGenericEventArgs<TK.CustomMap.Position> e)
        {
            Settings.Latto = String.Empty;
            Settings.Lngto = String.Empty;
            Settings.Placeto = String.Empty;
            var x = ToMap.MapCenter.Latitude;
            var y = ToMap.MapCenter.Longitude;
            var addresses = await Geocoding.GetPlacemarksAsync(x, y);
            var placemark = addresses?.FirstOrDefault();
            if (placemark != null)
            {
                if (addresses.FirstOrDefault().Thoroughfare != null)
                {
                    addlbl.Text = placemark.Thoroughfare + " , " + placemark.AdminArea + " , " + placemark.CountryName;
                    Settings.Latto = x.ToString();
                    Settings.Lngto = y.ToString();                    
                    Settings.Placeto = addlbl.Text;
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
        private async void Continuebtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }  
    }
}