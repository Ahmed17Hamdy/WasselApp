using Plugin.Connectivity;
using Plugin.Multilingual;
using System;
using System.Linq;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Helpers;
namespace WasselApp.Views.Intro
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroPage : ContentPage
	{
		public IntroPage ()
		{
			InitializeComponent ();
		}
        private async void UserButton_Cilcked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserPanel());
        }

        private async void DriverButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DriverPanel());
        }
        
      
        private async void Arabic_Clicked(object sender, EventArgs e)
        {
            Arabiclbl.TextColor = Color.Blue;
            Englishlbl.TextColor = Color.Black;
            arabicimg.IsVisible = true;
            Englishimg.IsVisible = false;
               Activ.IsRunning = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.EnglishName.Contains("Arabic"));
                AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
                Settings.LastUserGravity = "Arabic";
                GravityClass.Grav();

                App.Current.MainPage = new IntroPage();
            }
            else
            {
                await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
            }
              Activ.IsRunning = false;
        }

        private async void English_Clicked(object sender, EventArgs e)
        {
            Arabiclbl.TextColor = Color.Black;
            Englishlbl.TextColor = Color.Blue;
            arabicimg.IsVisible = false;
            Englishimg.IsVisible = true;
             Activ.IsRunning = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.NeutralCultureInfoList.ToList().First(element => element.EnglishName.Contains("English"));
                AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
                Settings.LastUserGravity = "English";
                App.Current.MainPage = new IntroPage();
            }
            else await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
              Activ.IsRunning = false;
        }
    }
}