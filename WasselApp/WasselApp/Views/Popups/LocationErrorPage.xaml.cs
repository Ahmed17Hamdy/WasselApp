using Plugin.Permissions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationErrorPage : PopupPage
    {
        private object x;

        public LocationErrorPage()
        {
            InitializeComponent();
            Errorlbl.Text = AppResources.LocationEnabled;
        }

        public LocationErrorPage(object x)
        {
            InitializeComponent();
            this.x = x;
            Errorlbl.Text = AppResources.PermissionLocationDetails;
            CrossPermissions.Current.OpenAppSettings();
        }
    }
}