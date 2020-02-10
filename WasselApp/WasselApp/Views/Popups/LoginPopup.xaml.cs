using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using WasselApp.Views.Panels;
using WasselApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Models;
using Plugin.Multilingual;

namespace WasselApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPopup : PopupPage
	{
        private object x;
        public LoginPopup (string data)
		{
			InitializeComponent ();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                 : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            if (data == "success")
            {
                Loginframe.BorderColor = Color.Blue;
                conditionlbl.IsVisible = true;
                conditionlbl.Text = AppResources.UserRegistered;
                conditionlbl.TextColor = Color.Blue;
            }
            else
            {
                Loginframe.BorderColor = Color.Red;
                Emaillbl.IsVisible = true;
                Emaillbl.TextColor = Color.Red;
                haveaccstk.IsVisible = true;
                Emaillbl.Text = AppResources.ChecEP;

            }
        }

        public LoginPopup(Data data)
        {
            InitializeComponent();
            Loginframe.BorderColor = Color.Red;
            if (data.email.ElementAt(0) == "The email has already been taken.")
            {
                Emaillbl.IsVisible = true;
                Emaillbl.TextColor = Color.Red;
                haveaccstk.IsVisible = true;
                Emaillbl.Text = AppResources.EmailTaken;
            }
        }
        private async void Login_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserPanel(x));
            await PopupNavigation.Instance.PopAsync();
        }
    }
}