using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WasselApp.Views.HomeMaster;
using Xamarin.Forms;
using WasselApp.Helpers;
using Xamarin.Forms.Xaml;

namespace WasselApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        
        public HomePage()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            masterPage.listView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }


    }
}