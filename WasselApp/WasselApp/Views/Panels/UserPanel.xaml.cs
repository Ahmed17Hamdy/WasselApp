using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Panels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPanel : ContentPage
	{
		public UserPanel ()
		{
			InitializeComponent ();
		}
        private void Login_Tapped(object sender, EventArgs e)
        {
            RegisterPanel.IsVisible = false;
            rgstimg.IsVisible = false;
            rgslbl.TextColor = Color.Black;
            lgnlbl.TextColor = Color.Blue;
            lgnimg.IsVisible = true;
            LoginPanel.IsVisible = true;
        }

        private void Register_Tapped(object sender, EventArgs e)
        {
            lgnlbl.TextColor = Color.Black;
            lgnimg.IsVisible = false;
            LoginPanel.IsVisible = false;
            RegisterPanel.IsVisible = true;
            rgstimg.IsVisible = true;
            rgslbl.TextColor = Color.Blue;
        }
    }
}