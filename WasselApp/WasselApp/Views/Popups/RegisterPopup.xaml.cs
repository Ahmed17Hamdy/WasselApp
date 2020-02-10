using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using WasselApp.Models;
using WasselApp.Helpers;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Multilingual;

namespace WasselApp.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPopup : PopupPage
	{
        private object x;

        public RegisterPopup (Data data )
		{
			InitializeComponent ();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            Registerframe.BorderColor = Color.Red;            
            if (data.email.ElementAt(0) == "The email has already been taken.")
            {
                Emaillbl.IsVisible = true;
                Emaillbl.TextColor = Color.Red;
                haveaccstk.IsVisible = true;
                Emaillbl.Text = AppResources.EmailTaken;
            }
        }
        public RegisterPopup(string data)
        {
            InitializeComponent();
            Registerframe.BorderColor = Color.Blue;
            if (data == "success")
            {
                conditionlbl.IsVisible = true;
                conditionlbl.Text = AppResources.UserRegistered;
                conditionlbl.TextColor = Color.Blue;
            }
        }   

        private async void Login_Tapped(object sender, EventArgs e)
        {            
            await Navigation.PushModalAsync(new UserPanel(x));
            await PopupNavigation.Instance.PopAsync();
        }
    }
}