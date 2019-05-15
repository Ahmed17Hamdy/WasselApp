using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Models;
namespace WasselApp.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPopup : PopupPage
	{
        private object x;
        public LoginPopup (string data)
		{
			InitializeComponent ();

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