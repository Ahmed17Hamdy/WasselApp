using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPopup : PopupPage
	{
		public LoginPopup (string data)
		{
			InitializeComponent ();
		}
	}
}