
using Com.OneSignal;
using WasselApp.Views.CarsPages;
using WasselApp.Views.HomeMaster;
using WasselApp.Views.Intro;
using WasselApp.Views.OrdersPage;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Helpers;
using WasselApp.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WasselApp
{
    public partial class App : Application
    {
        private Car car;

        public App()
        {
            InitializeComponent();
            OneSignal.Current.StartInit("f5f4f650-3453-456c-8024-010ea68e738b")
                  .EndInit();
            OneSignal.Current.IdsAvailable(IdsAvailable);
            //  MainPage = new NavigationPage( new Freightcars());
            MainPage = new NavigationPage( new OrderDetailsPage( car));
        }
        private void IdsAvailable(string userID, string pushToken)
        {
            Settings.LastSignalID = pushToken;
            Settings.UserFirebaseToken = userID;
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
