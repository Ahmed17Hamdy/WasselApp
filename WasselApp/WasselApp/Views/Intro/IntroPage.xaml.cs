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
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                    : FlowDirection.LeftToRight;
        }
        private async void UserButton_Cilcked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage( new UserPanel()),true);
        }
        private async void DriverButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new DriverPanel()),true);
        }    
        private async void Arabic_Clicked(object sender, EventArgs e)
        {
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