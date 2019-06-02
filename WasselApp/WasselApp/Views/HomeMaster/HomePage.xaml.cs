using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : Shell
    {
        //Random rand = new Random();
        //Dictionary<string, Type> routes = new Dictionary<string, Type>();
        //public Dictionary<string,Type> Routes { get { return routes; }  }
        //public ICommand HelpCommand => new Command<string>((url) => Device.OpenUri(new Uri (url)));
        //public ICommand RandomPageCommand => new Command(async () => await NavigateToRandomPageAsync());

        //private Task NavigateToRandomPageAsync()
        //{
        //    string destinationRoute = routes.ElementAt(rand.Next(0, routes.Count)).Key;
        //    string PageName = null;

        //}

        public HomePage()
        {
            InitializeComponent();
           
        }

       

      
    }
}