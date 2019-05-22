using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Helpers;
using Rg.Plugins.Popup.Services;

namespace WasselApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartypePage : PopupPage
    {
        public CartypePage()
        {
            InitializeComponent();
            Settings.Type = 1;
        }

        private void shahn_Tapped(object sender, EventArgs e)
        {
            shahnlbl.TextColor = Color.Blue;
            bricklbl.TextColor = Color.Gray;
            brickimg.IsVisible = false;
            shahnimg.IsVisible = true;
            Cartypesstk.IsVisible = true;
            Cartypesbrickstk.IsVisible = false;
            Carmodelsstk.IsVisible = false;
            Carmodelsbrickstk.IsVisible = false;
            Settings.Type = 1;
        }

        private void Waselbrick_Tapped(object sender, EventArgs e)
        {
            bricklbl.TextColor = Color.Blue;
            shahnlbl.TextColor = Color.Gray;
            brickimg.IsVisible = true;
            shahnimg.IsVisible = false;
            Cartypesstk.IsVisible = false;
            Cartypesbrickstk.IsVisible = true;
            Carmodelsstk.IsVisible = false;
            Carmodelsbrickstk.IsVisible = false;
            Settings.Type = 3;
        }


        private void CartypeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var CartypeSelected = CartypeList.SelectedItem as Cartype;
            Settings.Cartypename = CartypeSelected.name;
            Settings.Cartype = CartypeSelected.id;
            Carmodelsstk.IsVisible = true;
            Carmodelsbrickstk.IsVisible = false;
            Carmodelsstk.IsVisible = true;
            Modellist.ItemsSource = CartypeSelected.carmodals;

        }

        private void Cartypesbricklist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var CartypebrickSelected = Cartypesbricklist.SelectedItem as Cartype;
            Settings.Cartypename = CartypebrickSelected.name;
            Settings.Cartype = CartypebrickSelected.id;
            Carmodelsstk.IsVisible = false;
            Carmodelsbrickstk.IsVisible = true;
            Modelbricklist.ItemsSource = CartypebrickSelected.brickcarmodals;
        }

        private void Modellist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var carmodelitem = Modellist.SelectedItem as Carmodal;
            Settings.CarModelID = carmodelitem.id.ToString();
            Settings.Carmodelname = carmodelitem.name;
            Confirmbtn.IsEnabled = true;
        }

        private void Modelbricklist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var carmodelitem = Modelbricklist.SelectedItem as Carmodal;
            Settings.CarModelID = carmodelitem.id.ToString();
            Settings.Carmodelname = carmodelitem.name;
            Confirmbtn.IsEnabled = true;
        }

        private async void Confirmbtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}