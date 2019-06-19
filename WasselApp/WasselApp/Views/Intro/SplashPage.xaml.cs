using WasselApp.Helpers;
using WasselApp.Views.CarsPages;
using WasselApp.Views.Panels;
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
           
        }

        private void SetMainPage()
        {
            if (Settings.LastUsedID==0 && Settings.LastUsedDriverID==0)
            {
                Application.Current.MainPage =  new IntroPage();
            }
            else if (Settings.LastUsedID != 0 && Settings.ProfileUser == string.Empty)
            {
                Application.Current.MainPage = new IntroPage();
            }
            else if (Settings.LastUsedDriverID != 0 && Settings.ProfileUser == string.Empty)
            {
                Application.Current.MainPage = new IntroPage();
            }
            else if (Settings.LastUsedID == 0 && Settings.ProfileUser == "1")
            {
                Application.Current.MainPage = new UserPanel();
            }
            else if ( Settings.LastUsedID != 0 && Settings.ProfileUser == "1")
            {
                Application.Current.MainPage = new HomePage();
            }
            else if (Settings.ProfileUser == "2"  && Settings.LastUsedDriverID != 0)
            {
                Application.Current.MainPage = new MainTabbedPage();
            }
            else if (Settings.ProfileUser == "2" && Settings.LastUsedDriverID == 0)
            {
                Application.Current.MainPage = new DriverPanel();
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

           
            await splashImage.TranslateTo(0, 200,3000, Easing.BounceOut);
           
            SetMainPage();
            //  await splashImage.ScaleTo(150, 1200, Easing.Linear);
            //After loading  MainPage it gets Navigated to our new Page
        }
    }
}