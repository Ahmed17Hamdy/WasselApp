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
    public partial class ResidencesListView : PopupPage
    {
        public ResidencesListView()
        {
            InitializeComponent();
          //  Residencelst.ItemsSource= new[] { "a", "b", "c" };
        }

        private async void Residencelst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          var Selected=  Residencelst.SelectedItem as Resident;
            Settings.Residentid = Selected.id.ToString();
            Settings.ResidentName = Selected.name;
            await PopupNavigation.Instance.PopAsync();
        }
    }
}