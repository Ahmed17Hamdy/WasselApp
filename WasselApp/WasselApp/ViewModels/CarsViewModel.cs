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
            CartypebrickGetter();
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
        private ObservableCollection<Cartype> _cartypesbrick;
        public ObservableCollection<Cartype> CarTypesBrick
        {
            get
            {
                return _cartypesbrick;
            }
            set
            {
                _cartypesbrick = value;
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
        public async Task CartypebrickGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetCarsbricktype();
            foreach (var modelitem in ResBack)
            {

                var baramIcon = modelitem.icon;
                modelitem.icon = "http://waselksa.alsalil.net/users/images/" + baramIcon;
            }
            CarTypesBrick = ResBack;
        }

        public async Task CarGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetAllCars();
            Cars = ResBack;
            foreach (var item in Cars)
            {
                item.Position = new Position(Convert.ToDouble(item.Member.lat),
                    Convert.ToDouble(item.Member.lng));
                
                 if(item.Member.cartype == "1")
                {
                    item.cartypename = "صغيرة";                   
                }
                 else if (item.Member.cartype == "2")
                {
                    item.cartypename = "دينا";
                }
                else if (item.Member.cartype == "3")
                {
                    item.cartypename = "تريلة";
                }
                else if (item.Member.cartype == "4")
                {
                    item.cartypename = "سطحة";                   
                }
                
                item.Title = item.Member.name;                
                item.ShowCallout = true;
                if (item.Member != null)
                {
                    if (item.weight != int.Parse(item.Member.load) && item.cartypename == "دينا")
                    {
                        item.Image = "dinaredcar.png";
                    }
                    else if (item.weight != int.Parse(item.Member.load) && item.cartypename == "صغيرة")
                    {
                        item.Image = "smallredcar.png";
                    }
                    else if (item.weight != int.Parse(item.Member.load) && item.cartypename == "تريلة")
                    {
                        item.Image = "trilared.png";
                    }
                    else
                    {
                        item.Image = "satharedcar.png";
                    }
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
