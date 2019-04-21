using System;
using WasselApp.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}