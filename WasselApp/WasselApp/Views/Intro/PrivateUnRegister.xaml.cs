using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using WasselApp.Helpers;
using WasselApp.Models;
using WasselApp.ViewModels;
using WasselApp.Views.HomeMaster;
using WasselApp.Views.OrdersPage;
using WasselApp.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Intro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivateUnRegister : ContentPage
    {
        public PrivateUnRegister()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;
        }
        protected override bool OnBackButtonPressed()
        {

            return base.OnBackButtonPressed();
            _ = PopupNavigation.Instance.PopAsync();
        }

        private List<Car> _cars;

        public List<Car> Cars
        {
            get { return _cars; }
            set { _cars = value; OnPropertyChanged(); }
        }
        private List<Cartype> _carstypebrick;

        public List<Cartype> CarsTypeBrick
        {
            get { return _carstypebrick; }
            set { _carstypebrick = value; OnPropertyChanged(); }
        }
        private async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMilliseconds(5000));
            var location = await Geolocation.GetLocationAsync(request);
            MainMap.MapRegion = MapSpan.FromCenterAndRadius(
                    new Position(location.Longitude, location.Longitude), Distance.FromKilometers(50));
        }


        private async void StackTapped(object sender, EventArgs e)
        {
            MainMap.Pins = null;
            Activ.IsRunning = true;
            if (ModelFrame.IsVisible != true)
            {
                ModelFrame.IsVisible = true;
                ModelActive.IsRunning = true;
            }
            StackLayout f = (StackLayout)sender;
            string cartypenamestring = string.Empty;
            var fcontent = f.Children;
            var reqLabel = fcontent[0];
            var theLabel = reqLabel.GetType();
            if (theLabel == typeof(Label))
            {
                Label emailLabel = (Label)reqLabel;
                cartypenamestring = emailLabel.Text;
                CarsViewModel cars = new CarsViewModel();
                await cars.BrickCarGetter();
                Cars = cars.BrickCars.Where(o => o.cartypename == cartypenamestring).ToList();
                if (Cars.Count() != 0)
                {
                    Pinslbl.IsVisible = false;
                    MainMap.Pins = Cars;
                }
                else
                {
                    Pinslbl.IsVisible = true;
                }
                CarsTypeBrick = cars.CarTypesBrick.Where(o => o.name == cartypenamestring).ToList();
                foreach (var item in CarsTypeBrick)
                {
                    var CarModelList = item.brickcarmodals;
                    Modellist.ItemsSource = CarModelList;
                    ModelActive.IsRunning = false;
                }

            }

        }
        private void CloseFrame(object sender, EventArgs e)
        {
            ModelFrame.IsVisible = false;
            Activ.IsRunning = false;
        }
        private void Modellist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MainMap.Pins = null;
            var ModelSelected = Modellist.SelectedItem as Carmodal;
            var SelectedCars = Cars.Where(o => o.Member.carmodal == ModelSelected.id.ToString()).ToList();
            if (SelectedCars.Count() != 0)
            {
                Pinslbl.IsVisible = false;
                MainMap.Pins = SelectedCars;
            }
            else
            {
                Pinslbl.IsVisible = true;
            }
            Activ.IsRunning = false;

            ModelFrame.IsVisible = false;
        }

        private async void AllStack_Tapped(object sender, EventArgs e)
        {
            CarsViewModel cars = new CarsViewModel();
            await cars.BrickCarGetter();
            Cars = cars.BrickCars.ToList();
            MainMap.Pins = Cars;
            if (Cars.Count() != 0)
            {
                Pinslbl.IsVisible = false;
                MainMap.Pins = Cars;
            }
            else
            {
                Pinslbl.IsVisible = true;
            }
            Activ.IsRunning = false;

        }

        private async void MainMap_PinSelected(object sender, TKGenericEventArgs<TKCustomMapPin> e)
        {
            var _Carorder = MainMap.SelectedPin as Car;
            await PopupNavigation.Instance.PushAsync(new BrickCarDetailsPage(_Carorder));
        }
        private async void Regestrationbtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new IntroPage(), true);
        }

        private async void MyWay_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MyWayPage(), true);
        }

        private async void Placebtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UserPlacePage(), true);

        }
    }
}