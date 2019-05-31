using WasselApp.Helpers;
using WasselApp.Views.CarsPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Intro
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage
    {
      

        public SplashPage()
        {
            InitializeComponent();
            SetMainPage();
        }

        private void SetMainPage()
        {
            if (Settings.LastUsedID==0 && Settings.LastUsedDriverID==0 || 
                Settings.LastUsedID != 0 && Settings.LastUsedDriverID != 0)
            {
                Application.Current.MainPage = new NavigationPage(new IntroPage());
            }
            else if (Settings.LastUsedID != 0 && Settings.LastUsedDriverID == 0)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else if (Settings.LastUsedID == 0 && Settings.LastUsedDriverID != 0)
            {
                Application.Current.MainPage = new NavigationPage(new MainTabbedPage());
            }            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

           
            await splashImage.TranslateTo(0, 200,3000, Easing.BounceOut);
          //  await splashImage.ScaleTo(150, 1200, Easing.Linear);
              //After loading  MainPage it gets Navigated to our new Page
        }
    }
}