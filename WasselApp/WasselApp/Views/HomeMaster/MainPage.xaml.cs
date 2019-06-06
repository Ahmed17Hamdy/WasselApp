using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Views.CarsPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.HomeMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        private async void Shippingstk_Tapped(object sender, EventArgs e)
        {
          
            await Shell.Current.Navigation.PushModalAsync(new NavigationPage(new Freightcars()));
            
        }

        private async void Brick_Tapped(object sender, EventArgs e)
        {
         await   Shell.Current.Navigation.PushModalAsync(new PrivateCars(),true);
          //  await Navigation.PushAsync( new PrivateCars());
        }

        private async void CallUs_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new CallUsPage(),true);
        }

        private async void AboutUs_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new AboutUsPage(), true);
        }

        private async void OurGoals_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new OurGoals(), true);
        }

        private async void MyWay_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new CallUsPage(), true);
        }
    }
}