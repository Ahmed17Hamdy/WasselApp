using Plugin.Multilingual;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Helpers;
using WasselApp.Views.Intro;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoutPopup : ContentPage
	{
        public LogoutPopup()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                            : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Settings.LastUsedEmail = "";
            Settings.LastUsedID = 0;
            Settings.LastUsedDriverID = 0;
            Settings.LastUseeRole = 0;
            App.Current.MainPage = new UserPanel();
        //    PopupNavigation.Instance.PopAsync();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            App.Current.MainPage = new HomePage();
            //   PopupNavigation.Instance.PopAsync();
        }
    }
}
