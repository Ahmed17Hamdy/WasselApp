using Plugin.Multilingual;
using System;
using WasselApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Panels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPanel : ContentPage
	{
		public UserPanel ()
		{
			InitializeComponent ();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                 : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
        }
        public UserPanel(object x)
        {
            InitializeComponent();
            RegisterPanel.IsVisible = false;
            rgstimg.IsVisible = false;
            rgslbl.TextColor = Color.Black;
            lgnlbl.TextColor = Color.Blue;
            loginlbl.IsVisible = false;
            Registerlbl.IsVisible = true;
            lgnimg.IsVisible = true;
            LoginPanel.IsVisible = true;
        }
        private void Login_Tapped(object sender, EventArgs e)
        {
            RegisterPanel.IsVisible = false;
            rgstimg.IsVisible = false;
            rgslbl.TextColor = Color.Black;
            lgnlbl.TextColor = Color.Blue;
            loginlbl.IsVisible = false;
            Registerlbl.IsVisible = true;
            lgnimg.IsVisible = true;
            LoginPanel.IsVisible = true;
        }

        private void Register_Tapped(object sender, EventArgs e)
        {
            lgnlbl.TextColor = Color.Black;
            lgnimg.IsVisible = false;
            LoginPanel.IsVisible = false;
            RegisterPanel.IsVisible = true;
            loginlbl.IsVisible = true;
            Registerlbl.IsVisible = false;
            rgstimg.IsVisible = true;
            rgslbl.TextColor = Color.Blue;
        }
        private async void Recovery_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoveryPagesForUsers(), true);
        }
    }
}