using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected override async void OnAppearing()
        {
            base.OnAppearing();

           
            await splashImage.TranslateTo(0, 200,3000, Easing.BounceOut);
          //  await splashImage.ScaleTo(150, 1200, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new IntroPage());    //After loading  MainPage it gets Navigated to our new Page
        }
    }
}