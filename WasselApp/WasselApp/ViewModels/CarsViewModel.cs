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
using System.Linq;

namespace WasselApp.ViewModels
{
    public class CarsViewModel : INotifyPropertyChanged

    {
        public CarsViewModel()
        {
            _ = CartypeGetter();
            _ = CartypebrickGetter();
            _ = ShippingCarGetter();
            _ = BrickCarGetter();
        }
       
        private ObservableCollection<Car> _shippingcars;
        public ObservableCollection<Car> ShippingCars
        {
            get
            {
                return _shippingcars;
            }
            set
            {
                _shippingcars = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Car> _brickcars;
        public ObservableCollection<Car> BrickCars
        {
            get
            {
                return _brickcars;
            }
            set
            {
                _brickcars = value;
                OnPropertyChanged();
            }
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

        public async Task ShippingCarGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetAllShippingCars();
            ShippingCars = new ObservableCollection<Car>(ResBack);
            foreach (var item in ShippingCars)
            {               
                item.Position = new Position(Convert.ToDouble(item.Member.lat),
                    Convert.ToDouble(item.Member.lng));
                if (item.cartypename == null)
                {
                    switch (item.Member.cartype)
                    {
                        case "1":
                            item.cartypename = "صغيرة";
                            break;
                        case "2":
                            item.cartypename = "دينا";
                            break;
                        case "3":
                            item.cartypename = "تريلة";
                            break;
                        case "4":
                            item.cartypename = "سطحة";
                            break;
                    }

                    
                                     
                }                
                item.Title = item.Member.name;
                item.ShowCallout = true;
                if(item.Order!= null)
                {
                    if (item.Order.weight != null)
                    {
                        if (item.Order.weight >= 0.9 * int.Parse(item.Member.load) && item.cartypename == "دينا")
                        {
                            item.Image = "dinared.png";
                        }
                        else if (item.Order.weight >= 0.25 * int.Parse(item.Member.load)
                             && item.Order.weight < 0.9 * int.Parse(item.Member.load) && item.cartypename == "دينا")
                        {
                            item.Image = "dinablue.png";
                        }
                        else if (item.Order.weight >= 0.9 * int.Parse(item.Member.load) && item.cartypename == "صغيرة")
                        {
                            item.Image = "smallred.png";
                        }
                        else if (item.Order.weight >= (0.25 * int.Parse(item.Member.load)) &&
                            item.Order.weight < 0.9 * int.Parse(item.Member.load) && item.cartypename == "صغيرة")
                        {
                            item.Image = "smallblue.png";
                        }
                        else if (item.Order.weight >= 0.9 * int.Parse(item.Member.load) && item.cartypename == "تريلة")
                        {
                            item.Image = "trilared.png";
                        }
                        else if (item.Order.weight >= (0.25 * int.Parse(item.Member.load)) &&
                            item.Order.weight < (0.9 * int.Parse(item.Member.load)) && item.cartypename == "تريلة")
                        {
                            item.Image = "trilblue.png";
                        }
                        else if (item.Order.weight >= (0.9 * int.Parse(item.Member.load)) && item.cartypename == "سطحة")
                        {
                            item.Image = "sathared.png";
                        }
                        else if (item.Order.weight >= (0.25 * int.Parse(item.Member.load)) &&
                            item.Order.weight < (0.9 * int.Parse(item.Member.load)) && item.cartypename == "سطحة")
                        {
                            item.Image = "sathablue.png";
                        }
                        else
                        {
                            if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "دينا")
                            {
                                item.Image = "dinagreen.png";
                            }
                            else if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "صغيرة")
                            {
                                item.Image = "smallgreen.png";
                            }
                            else if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "تريلة")
                            {
                                item.Image = "trilagreen.png";
                            }
                            else if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "سطحة")
                            {
                                item.Image = "sathagreen.png";
                            }                            
                        }
                    }
                    else
                    {
                        if (item.cartypename == "دينا")
                        {
                            item.Image = "dinagreen.png";
                        }
                        else if (item.cartypename == "صغيرة")
                        {
                            item.Image = "smallgreen.png";
                        }
                        else if (item.cartypename == "تريلة")
                        {
                            item.Image = "trilagreen.png";
                        }
                        else if (item.cartypename == "سطحة")
                        {
                            item.Image = "sathagreen.png";
                        }                       
                    }
                }
                else
                {
                    if (item.cartypename == "دينا")
                    {
                        item.Image = "dinagreen.png";
                    }
                    else if (item.cartypename == "صغيرة")
                    {
                        item.Image = "smallgreen.png";
                    }
                    else if (item.cartypename == "تريلة")
                    {
                        item.Image = "trilagreen.png";
                    }
                    else if (item.cartypename == "سطحة")
                    {
                        item.Image = "sathagreen.png";
                    }
                    
                }
            }
                 
        }

        public async Task BrickCarGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetAllBrickCars();
            BrickCars = new ObservableCollection<Car>(ResBack);
            foreach (var item in BrickCars)
            {

                item.Position = new Position(Convert.ToDouble(item.Member.lat),
                    Convert.ToDouble(item.Member.lng));
                if (item.cartypename == null)
                {

                    if (item.Member.cartype == "5")
                    {
                        item.cartypename = "لانسر";
                    }
                    else if (item.Member.cartype == "6")
                    {
                        item.cartypename = "تيوتا";
                    }
                    else if (item.Member.cartype == "7")
                    {
                        item.cartypename = "نيسان";
                    }
                    else if (item.Member.cartype == "8")
                    {
                        item.cartypename = "هيونداي";
                    }
                    else if (item.Member.cartype == "0")
                    {
                        item.cartypename = "أودي";
                    }
                }
                item.Title = item.Member.name;
                item.ShowCallout = true;
                if (item.Order != null)
                {
                    if (item.Order.weight != null)
                    {

                        if (item.Order.weight >= (0.9 * int.Parse(item.Member.load)) && item.cartypename == "لانسر")
                        {
                            item.Image = "lancerred.png";
                        }
                        else if (item.Order.weight >= (0.25 * int.Parse(item.Member.load))
                             && item.Order.weight < (0.9 * int.Parse(item.Member.load)) && item.cartypename == "لانسر")
                        {
                            item.Image = "lancerblue.png";
                        }
                        else if (item.Order.weight >= (0.9 * int.Parse(item.Member.load)) && item.cartypename == "تيوتا")
                        {
                            item.Image = "toyotared.png";
                        }
                        else if (item.Order.weight >= (0.25 * int.Parse(item.Member.load)) &&
                            item.Order.weight < (0.9 * int.Parse(item.Member.load)) && item.cartypename == "تيوتا")
                        {
                            item.Image = "toyotablue.png";
                        }
                        else if (item.Order.weight >= (0.9 * int.Parse(item.Member.load)) && item.cartypename == "نيسان")
                        {
                            item.Image = "nissanred.png";
                        }
                        else if (item.Order.weight >= (0.25 * int.Parse(item.Member.load)) &&
                            item.Order.weight < 0.9 * int.Parse(item.Member.load) && item.cartypename == "نيسان")
                        {
                            item.Image = "nissanblue.png";
                        }
                        else if (item.Order.weight >= 0.9 * double.Parse(item.Member.load) && item.cartypename == "هيونداي")
                        {
                            item.Image = "hyundaired.png";
                        }
                        else if (item.Order.weight >= 0.25 * double.Parse(item.Member.load) &&
                            item.Order.weight < 0.9 * double.Parse(item.Member.load) && item.cartypename == "هيونداي")
                        {
                            item.Image = "hyundaiblue.png";
                        }
                        else if (item.Order.weight >= 0.9 * double.Parse(item.Member.load) && item.cartypename == "أودي")
                        {
                            item.Image = "hyundaired.png";
                        }
                        else if (item.Order.weight >= 0.25 * double.Parse(item.Member.load) &&
                            item.Order.weight < 0.9 * double.Parse(item.Member.load) && item.cartypename == "أودي")
                        {
                            item.Image = "hyundaiblue.png";
                        }
                        else
                        {

                            if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "لانسر")
                            {
                                item.Image = "lancergreen.png";
                            }
                            else if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "تيوتا")
                            {
                                item.Image = "toyotagreen.png";
                            }
                            else if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "نيسان")
                            {
                                item.Image = "nissangreen.png";
                            }
                            else if (item.Order.weight < 0.25 * double.Parse(item.Member.load) && item.cartypename == "هيونداي")
                            {
                                item.Image = "hyundaigreen.png";
                            }
                            else
                            {
                                item.Image = "hyundaigreen.png";
                            }
                        }
                    }
                    else
                    {

                        if (item.cartypename == "لانسر")
                        {
                            item.Image = "lancergreen.png";
                        }
                        else if (item.cartypename == "تيوتا")
                        {
                            item.Image = "toyotagreen.png";
                        }
                        else if (item.cartypename == "نيسان")
                        {
                            item.Image = "nissangreen.png";
                        }
                        else if (item.cartypename == "هيونداي")
                        {
                            item.Image = "hyundaigreen.png";
                        }
                        else if (item.cartypename == "أودي")
                        {
                            item.Image = "hyundaigreen.png";
                        }
                    }
                }
                else
                {


                     if (item.cartypename == "لانسر")
                    {
                        item.Image = "lancergreen.png";
                    }
                    else if (item.cartypename == "تيوتا")
                    {
                        item.Image = "toyotagreen.png";
                    }
                    else if (item.cartypename == "نيسان")
                    {
                        item.Image = "nissangreen.png";
                    }
                    else if (item.cartypename == "هيونداي")
                    {
                        item.Image = "hyundaigreen.png";
                    }
                    else if (item.cartypename == "أودي")
                    {
                        item.Image = "hyundaigreen.png";
                    }
                }
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}