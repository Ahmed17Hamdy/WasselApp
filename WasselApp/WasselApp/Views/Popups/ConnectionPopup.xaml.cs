using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using WasselApp.Helpers;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnectionPopup : PopupPage
	{
        private object x;
        private int y;
        public ConnectionPopup ()
		{
			InitializeComponent ();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            Connectionlbl.IsVisible = true;
            FieldsRequiredlbl.IsVisible = false;
		}

        public ConnectionPopup(object x)
        {
            this.x = x;
           
            InitializeComponent();
            FieldsRequiredlbl.IsVisible = true;
            Connectionlbl.IsVisible = false;
        }
        public ConnectionPopup(int y)
        {
            this.y= y;

            InitializeComponent();

            LocationRequiredlbl.IsVisible = true;
            FieldsRequiredlbl.IsVisible = false;
            Connectionlbl.IsVisible = false;
            CrossPermissions.Current.OpenAppSettings();
            GetLocation();
        }

        private async void GetLocation()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus != PermissionStatus.Granted)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);
                Settings.LastLat = location.Latitude.ToString();
                Settings.LastLng = location.Longitude.ToString();
            }

            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionLocationDetails,
                    AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
            }
        }
    }
