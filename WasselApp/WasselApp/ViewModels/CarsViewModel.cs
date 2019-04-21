using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using WasselApp.Services;
using TK.CustomMap;
namespace WasselApp.ViewModels
{
    public class CarsViewModel : INotifyPropertyChanged

    {
        public CarsViewModel()
        {
            CartypeGetter();
             CarGetter();
        }
        private ObservableCollection<Car> _cars;
        public ObservableCollection<Car> Cars
        {
            get
            {
                return _cars;
            }
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Cartype> _cartypes;
        public ObservableCollection<Cartype> CarTypes
        {
            get
            {
                return _cartypes;
            }
            set
            {
                _cartypes = value;
                OnPropertyChanged();
            }
        }
        public async Task CartypeGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetCarstype();
            foreach (var modelitem in ResBack)
            {

                var baramIcon = modelitem.icon;
                modelitem.icon = "http://waselksa.alsalil.net/users/images/" + baramIcon;
            }
            CarTypes = ResBack;
        }

        public async Task CarGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetAllCars();
            Cars = ResBack;
            foreach (var item in Cars)
            {
                item.Position = new Position(Convert.ToDouble(item.lat), Convert.ToDouble(item.lng));
                if (item.cartype==2)
                {
                    item.cartypename = "دينا";                   
                }
                else if(item.cartype==1)
                {
                    item.cartypename = "صغيرة";                   
                }
                else if (item.cartype == 3)
                {
                    item.cartypename = "تريلة";
                }
                else if (item.cartype == 4)
                {
                    item.cartypename = "سطحة";                   
                }
                
                item.Title = item.name;                
                item.ShowCallout = true;
                if(item.cartypename == "دينا")
                {
                    item.Image = "dinaredcar.png";
                }
                else if(item.cartypename == "صغيرة")
                {
                    item.Image = "smallredcar.png";
                }
                else if(item.cartypename == "تريلة")
                {
                    item.Image = "trilared.png";
                }
                else
                {
                    item.Image = "satharedcar.png";
                }


                //  item.Subtitle = item.carmodalname;
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
