
using WasselApp.Views.CarsPages;
using WasselApp.Views.HomeMaster;
using WasselApp.Views.Intro;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WasselApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //  MainPage = new NavigationPage( new Freightcars());
            MainPage = new HomePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
