using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Views.CarsPages;
using WasselApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Multilingual;
using WasselApp.Views.Intro;

namespace WasselApp.Views.HomeMaster
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                   : FlowDirection.LeftToRight;         
        }

        private async void Shippingstk_Tapped(object sender, EventArgs e)
        {
          if(Settings.LastUsedID != 0)
            {
                await Navigation.PushAsync(new NavigationPage(new Freightcars()), true);

            }
          else
            {
                await Navigation.PushModalAsync(new FrieghtUnRegister(), true);
            }
        }

        private async void Brick_Tapped(object sender, EventArgs e)
        {
            // await   Shell.Current.Navigation.PushModalAsync(new PrivateCars(),true);
            if (Settings.LastUsedID != 0)
            {
                await Navigation.PushAsync(new NavigationPage(new PrivateCars()), true);
            }
            else
            {
                await Navigation.PushModalAsync(new PrivateUnRegister(), true);
            }           
            //  await Navigation.PushAsync( new PrivateCars());
        }

        private async void CallUs_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new CallUsPage()), true);
         //   await Shell.Current.Navigation.PushAsync(new CallUsPage(),true);
        }

        private async void AboutUs_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new CallUsPage()), true);
         //   await Shell.Current.Navigation.PushAsync(new AboutUsPage(), true);
        }

        private async void OurGoals_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new OurGoals()), true);
         //   await Shell.Current.Navigation.PushAsync(new OurGoals(), true);
        }

        private async void MyWay_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new CallUsPage()), true);
         //   await Shell.Current.Navigation.PushAsync(new CallUsPage(), true);
        }
    }
}