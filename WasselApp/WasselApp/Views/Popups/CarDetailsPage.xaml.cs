using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using WasselApp.Views.OrdersPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Helpers;
using Rg.Plugins.Popup.Services;
using WasselApp.Views.Intro;

namespace WasselApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarDetailsPage : PopupPage
    {
        Car CarOrder;
        public CarDetailsPage(Models.Car _Carorder)
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            CarOrder = _Carorder;
            Settings.CarLat = Convert.ToDouble(CarOrder.Member.lat);
            Settings.CarLng = Convert.ToDouble(CarOrder.Member.lng);
            if (CarOrder.Order != null)
            {
                Fromlbl.Text = _Carorder.Order.addressfrom;
                Tolbl.Text = _Carorder.Order.addressto;
            }
            else
            {
                Fromlbl.Text = AppResources.NoPlaceFrom;
                Tolbl.Text = AppResources.NoPlaceTo;
            }
            
        }
        private async void Orderbtn_Clicked(object sender, EventArgs e)
        {
            if (Settings.LastUsedID != 0)
            {
                await Navigation.PushModalAsync(new NavigationPage(new OrderDetailsPage(CarOrder)));
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
               // await Navigation.PushModalAsync(new NavigationPage(new IntroPage()), true);
                await PopupNavigation.Instance.PopAsync();
                App.Current.MainPage = new NavigationPage(new IntroPage());
            }
     
        }
        protected  override bool OnBackButtonPressed()
        {
            
            return base.OnBackButtonPressed();
            _ = PopupNavigation.Instance.PopAsync();
        }
      
    }
}