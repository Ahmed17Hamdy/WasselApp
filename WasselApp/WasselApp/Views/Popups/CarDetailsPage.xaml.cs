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
namespace WasselApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarDetailsPage : PopupPage
    {
        Car CarOrder;
        public CarDetailsPage(Models.Car _Carorder)
        {
            InitializeComponent();
            CarOrder = _Carorder;
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
            await Navigation.PushAsync(new OrderDetailsPage(CarOrder));
        }
    }
}