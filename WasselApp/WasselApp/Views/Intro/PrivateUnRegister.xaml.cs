﻿using Plugin.Connectivity;
using Plugin.Multilingual;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            GetCars();
            GetLocation();
        }

        private async void GetCars()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CarsViewModel cars = new CarsViewModel();
                await cars.BrickCarGetter();
                Cars = cars.BrickCars.ToList();
                CarTypeBrick = cars.CarTypesBrick.ToList();
            }
            else
            {
                Errorlbl.IsVisible = true;
                Errorlbl.Text = AppResources.ErrorMessage;
            }
        }

        private List<Car> _cars;

        public List<Car> Cars
        {
            get { return _cars; }
            set { _cars = value; OnPropertyChanged(); }
        }
        private List<Cartype> _cartypebrick;

        public List<Cartype> CarTypeBrick
        {
            get { return _cartypebrick; }
            set { _cartypebrick = value; OnPropertyChanged(); }
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
                    await PopupNavigation.Instance.PushAsync(new LocationErrorPage());
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new LocationErrorPage(x));

                //On iOS you may want to send your user to the settings screen.
             //   CrossPermissions.Current.OpenAppSettings();
            }
        }

        private bool _usertabbed;
        private object x;

        private  void StackTapped(object sender, EventArgs e)
        { 
            if(ModelFrame.IsVisible == true)
            {
                ModelFrame.IsVisible = false;
                ModelActive.IsRunning = false;
            }
            
            StackLayout f = (StackLayout)sender;
            f.IsEnabled = false;
            string cartypenamestring = string.Empty;
            var fcontent = f.Children;
             var reqLabel = fcontent[0];
            var theLabel = reqLabel.GetType();
            if (theLabel == typeof(Label))
            {
                Label emailLabel = (Label)reqLabel;

                cartypenamestring = emailLabel.Text;
            }
            Activ.IsRunning = true;
            if (ModelFrame.IsVisible != true)
            {
                ModelFrame.IsVisible = true;
                ModelActive.IsRunning = true;
               
            var    CarTypesBrick = CarTypeBrick.Where(o => o.name == cartypenamestring).ToList();
              
                foreach (var item in CarTypesBrick)
                {
                    var CarModelList = item.brickcarmodals;
                    Modellist.ItemsSource = CarModelList;
                }
                ModelActive.IsRunning = false;
                Activ.IsRunning = false;
                f.IsEnabled = true;
            }
            else
            {
                ModelFrame.IsVisible = true;
                ModelActive.IsRunning = true;               
             var CarTypesBrick = CarTypeBrick.Where(o => o.name == cartypenamestring).ToList();
                foreach (var item in CarTypesBrick)
                {
                    var CarModelList = item.brickcarmodals;
                    Modellist.ItemsSource = CarModelList;
                }
                ModelActive.IsRunning = false;
                Activ.IsRunning = false;
                f.IsEnabled = true;
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
            var SelectedCars = Cars.Where(o => o.Member.carmodal == ModelSelected.parent.ToString()).ToList();
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
            if (ModelFrame.IsVisible == true)
            {
                ModelFrame.IsVisible = false;
                ModelActive.IsRunning = false;
            }
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