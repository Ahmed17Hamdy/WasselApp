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
	public partial class RegisterPopup : PopupPage
	{
		public RegisterPopup (string data)
		{
			InitializeComponent ();
            if(data == "success")
            {
                data = "You have Registered Successfully !!!";
                conditionlbl.Text = data;
            }
            else
            {
                conditionlbl.Text = data;
            }
          
		}
	}
}