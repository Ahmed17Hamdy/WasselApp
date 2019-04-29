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
            await Navigation.PushAsync(new Freightcars());
        }

        private async void Brick_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync( new PrivateCars());
        }
    }
}