using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using TK.CustomMap.Api.Google;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Helpers;
using WasselApp.ViewModels;
using System.Collections.ObjectModel;
using TK.CustomMap.Overlays;
using TK.CustomMap.Api;
using Xamarin.Essentials;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Multilingual;

namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceToPage : ContentPage
    {
        ObservableCollection<TKRoute> routes;
        ObservableCollection<TKCustomMapPin> Pins = new ObservableCollection<TKCustomMapPin>();
        ObservableCollection<TKCustomMapPin> pins;
        MapSpan bounds;
        public PlaceToPage()
        {

            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            Fromplacecc._autoCompleteListView.ItemSelected += ItemSelected; 
            GmsPlace.Init("AIzaSyB7rB6s8fc317zCPz8HS_yqwi7HjMsAqks");
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
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location != null)
                    {
                        ToMap.MoveToMapRegion(MapSpan.FromCenterAndRadius(
                       new Position(location.Longitude, location.Longitude), Distance.FromKilometers(50)), true);
                    }
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

        async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
            if (e.SelectedItem == null) return;
            var prediction = (IPlaceResult)e.SelectedItem;
            try
            {
                var Address = prediction.Description;
                 addlbl.Text= Address;
                Settings.Placeto = addlbl.Text;
                Orderbtn.IsVisible = true;
                var locations = await Geocoding.GetLocationsAsync(Address);
                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Settings.Latto = location.Latitude.ToString();
                    Settings.Lngto = location.Longitude.ToString();                   
                    var newPin = new TKCustomMapPin
                    {
                        Position = new Position(location.Latitude, location.Longitude),
                        Title = "Cluster Test",
                        Image = "placeholder.png"
                    };
                    Pins.Clear();
                    Pins.Add(newPin);
                    ToMap.Pins = Pins;
                    ToMap.MoveToMapRegion ( MapSpan.FromCenterAndRadius(
                    new Position(location.Longitude, location.Longitude),Distance.FromKilometers(50)),true);
                    Orderbtn.IsVisible = true;
                }
            }
            catch (Exception ex)
            {

            }

           // HandleItemSelected(prediction);
        }

        private async void AddPinMap_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPinOnMapPage(), true);
        }

        private async void Orderbtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();            
        }
    }
}