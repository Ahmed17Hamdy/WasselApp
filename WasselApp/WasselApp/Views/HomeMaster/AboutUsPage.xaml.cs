using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.HomeMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;

        }
    }
}