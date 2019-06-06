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

namespace WasselApp.Views.OrdersPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceToPage : ContentPage
    {
        ObservableCollection<TKRoute> routes;
        ObservableCollection<TKCustomMapPin> Pins = new ObservableCollection<TKCustomMapPin>();
        ObservableCollection<TKCustomMapPin> pins;
        MapSpan bounds;
        public PlaceToPage(string locationto)
        {

            InitializeComponent();
            //BindingContext = new AddRouteViewModel( routes,
            //pins,  bounds);
            //Fromplacecc.Bounds = bounds;
            Fromplacecc._autoCompleteListView.ItemSelected += ItemSelected; 

            GmsPlace.Init("AIzaSyB7rB6s8fc317zCPz8HS_yqwi7HjMsAqks");
        }
        public Button btn { get { return Orderbtn; } }
        public string latto;
        public string lngto;
        public string location;
        private async void MainMap_UserLocationChanged(object sender, TK.CustomMap.TKGenericEventArgs<TK.CustomMap.Position> e)
        {

            var x = ToMap.MapCenter.Latitude;
            var y = ToMap.MapCenter.Longitude;            
            var addresses = await Geocoding.GetPlacemarksAsync(x, y);
            var placemark = addresses?.FirstOrDefault();
            if (placemark != null)
            {
                if (addresses.FirstOrDefault().Thoroughfare != null )
                {
                    addlbl.Text = placemark.Thoroughfare + " , " + placemark.AdminArea + " , " + placemark.CountryName;
                    Settings.Latto = latto = x.ToString();
                    Settings.Lngto = lngto = y.ToString();
                    Settings.Placeto = location = addlbl.Text;
                    Orderbtn.IsVisible = true;
                }
                else
                {
                    addlbl.Text = AppResources.LocationNotFound;
                    Orderbtn.IsVisible = false;
                }
            }
            else
            {
                addlbl.Text = AppResources.LocationNotFound;
            }

        }
        
        async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Settings.Placeto = String.Empty;
            if (e.SelectedItem == null) return;
            var prediction = (IPlaceResult)e.SelectedItem;
            try
            {
                var Address = prediction.Description;
                Settings.Placeto = Address;
                adlbl.IsVisible = true;
                addlbl.IsVisible = false;
                adlbl.Text = Settings.Placeto;
                Settings.Placeto = addlbl.Text;
                Orderbtn.IsVisible = true;
                var locations = await Geocoding.GetLocationsAsync(Address);
                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    Settings.Latto = location.Latitude.ToString();
                    Settings.Lngto = location.Longitude.ToString();
                    ToPlaceimg.IsVisible = false;
                    var newPin = new TKCustomMapPin
                    {
                        Position = new Position(location.Latitude, location.Longitude),
                        Title = "Cluster Test"
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
       
    }
}