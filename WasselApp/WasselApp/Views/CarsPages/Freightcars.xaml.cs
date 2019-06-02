﻿using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using WasselApp.Models;
using WasselApp.ViewModels;
using WasselApp.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.CarsPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Freightcars : ContentPage
	{
        
        public Freightcars ()
		{
			InitializeComponent ();
            GetLocation();
           // GetCars();
        }

        private async void GetCars()
        {
            CarsViewModel carsViewModel = new CarsViewModel();
            await carsViewModel.ShippingCarGetter();
          Cars= carsViewModel.ShippingCars.ToList();
           
        }

        private List<Car> _cars;

        public List<Car> Cars
        {
            get { return _cars; }
            set { _cars = value; OnPropertyChanged(); }
        }
        private List<Cartype> _carstype;

        public List<Cartype> CarsType
        {
            get { return _carstype; }
            set { _carstype = value; OnPropertyChanged(); }
        }
        private async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);
            MainMap.MapRegion=  MapSpan.FromCenterAndRadius(
                    new Position(location.Longitude,location.Longitude), Distance.FromMiles(1));
        }

        private async void All_Tapped(object sender, EventArgs e)
        {
           
            Label Alllbl = (Label)sender;
               string citem = Alllbl.Text;
            CarsViewModel cars = new CarsViewModel();
            await cars.ShippingCarGetter();
            Cars = cars.ShippingCars.ToList();
            MainMap.Pins = Cars ;

        }

        private async void namelblTabbed(object sender, EventArgs e)
        {
            MainMap.Pins = null;
            Label Alllbl = (Label)sender;
            string citem = Alllbl.Text;
            CarsViewModel cars = new CarsViewModel();
            await cars.ShippingCarGetter();
            MainMap.Pins = cars.ShippingCars.Where(o => o.cartypename == citem).ToList();
        }

        private async void StackTapped(object sender, EventArgs e)
        {
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
                await cars.ShippingCarGetter();
                Cars = cars.ShippingCars.Where(o => o.cartypename == cartypenamestring).ToList();
                MainMap.Pins = Cars;
                CarsType= cars.CarTypes.Where(o => o.name == cartypenamestring).ToList();
                foreach (var item in CarsType)
                {
                    var CarModelList = item.carmodals;
                    foreach (var modelitem in CarModelList)
                    {
                        var ModelIcon = modelitem.icon;
                        modelitem.icon = "http://waselksa.alsalil.net/users/images/" + ModelIcon;
                    }
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
          
            var ModelSelected = Modellist.SelectedItem as Carmodal;
            var SelectedCars = Cars.Where(o => o.Member.carmodal == ModelSelected.id.ToString()).ToList();
            
            Activ.IsRunning = false;

            ModelFrame.IsVisible = false;
        }

        private async void AllStack_Tapped(object sender, EventArgs e)
        {
            Activ.IsRunning = true;
            if (ModelFrame.IsVisible == true)
            {
                ModelFrame.IsVisible = false;
                ModelActive.IsRunning = false;
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
               await cars.ShippingCarGetter();
                Cars = cars.ShippingCars.ToList();
                MainMap.Pins = Cars;
                Activ.IsRunning = false;
            }
        }

        private async void MainMap_PinSelected(object sender, TKGenericEventArgs<TKCustomMapPin> e)
        {
            var _Carorder = MainMap.SelectedPin as Car;
            await PopupNavigation.Instance.PushAsync(new CarDetailsPage(_Carorder));
        }
    }
}