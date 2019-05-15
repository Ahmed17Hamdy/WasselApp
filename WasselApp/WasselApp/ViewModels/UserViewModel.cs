using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WasselApp.Models;
using WasselApp.Services;
using WasselApp.Views.CarsPages;
using WasselApp.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using WasselApp.Helpers;
namespace WasselApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public Cartype cartype { get; set; }

        public UserViewModel()
        {
            //    AddToCarTypeList();
        }
        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning == value)
                    return;
                _isRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }
        UserService userService = new UserService();
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string carnumber { get; set; }
        public string carmodal { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
        public string age { get; set; }
        public string national { get; set; }
        public string image { get; set; }
        public string idnumber { get; set; }
        public string denominationnumber { get; set; }
        public string passportnumber { get; set; }
        public string nationality { get; set; }
        public string type { get; set; }
        public string password { get; set; }
        public string confirmpass { get; set; }
        public string firebase_token { get; set; }
        public string device_id { get; set; }
        public string lat { get; set; }

        public StreamImageSource Img { get; set; }
        public string lng { get; set; }

        private void IdsAvailable(string userID, string pushToken)
        {
            Settings.LastSignalID = pushToken;
            Settings.UserFirebaseToken = userID;

        }
        public ICommand UserRegisterCommand => new Command(async () =>
        {
            IsRunning = true;
            var location = await Geolocation.GetLocationAsync();
            var device = DeviceInfo.Model;
            User _userReg = new User
            {
                email = email,
                name = name,
                password = password,
                confirmpass = confirmpass,
                firebase_token = "35",
                device_id = "111.2225.555",
                lat = location.Latitude.ToString(),
                lng = location.Longitude.ToString()

            };
            var ResBack = await userService.RegisterAsync(_userReg);
           
            if (ResBack == "false")
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
            }
            else
            {

                try
                {
                    try
                    {
                        var JsonResponse = JsonConvert.DeserializeObject<RegisterResponse>(ResBack);
                        if (JsonResponse.success == false)
                            IsRunning = false;
                            await PopupNavigation.Instance.PushAsync(new RegisterPopup(JsonResponse.data));
                    }
                    catch
                    {
                        var JsonResponse = JsonConvert.DeserializeObject<Response<string, User>>(ResBack);
                        if (JsonResponse.success == true)
                            IsRunning = false;
                        await PopupNavigation.Instance.PushAsync(new RegisterPopup(JsonResponse.data));
                        Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new HomePage());
                    }                  
                }
                catch (Exception)
                {

                    await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
                    return;
                }
            }

        });
        public ICommand LoginCommand => new Command(async () =>
        {
         //   OneSignal.Current.IdsAvailable(IdsAvailable);

            User user = new User {
                name = name,
            email = email,
            password = password,
            confirmpass = confirmpass,
            firebase_token = "3333333" /*Settings.UserFirebaseToken*/,
            device_id = "0",
        };
            var ResBack = await userService.LoginCommandAsync(user);
            if (ResBack == "false")
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
            }
            else
            {

                try
                {
                    try
                    {
                        var JsonResponse = JsonConvert.DeserializeObject<RegisterResponse>(ResBack);
                        if (JsonResponse.success == false)
                            IsRunning = false;
                        await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
                    }
                    catch
                    {
                        var JsonResponse = JsonConvert.DeserializeObject<Response<string, User>>(ResBack);
                        if (JsonResponse.success == true)
                        {
                            IsRunning = false;
                            var _userID = JsonResponse.message.id;

                            Settings.LastUsedID = _userID;
                            Settings.LastUsedEmail = JsonResponse.message.email;
                            await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
                            Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new HomePage());
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
                        }
                    }         
                }
                catch (Exception)
                {

                    await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
                    return;
                }
            }
            // email = Settings.LastUsedEmail;
            //if (ResBack == "false")
            //{
            //    await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
            //}
            //else
            //{
            //    bool checker = false;
            //    try
            //    {

            //        //var JsonResponse = JsonConvert.DeserializeObject<Response<string, User>>(ResBack);
            //        //if (JsonResponse.success == true)
            //        //{
            //        //    var _userID = JsonResponse.message.id;
            //        //    checker = true;
            //        //    // Settings.LastUsedID = _userID;
            //        //    //  Settings.LastUsedEmail = EntryEmail.Text;
            //        //    await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
            //        //    Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new HomePage());
            //        //}
            //        //else
            //        //{
            //        //    await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
            //        //}
            //    }
            //    catch (Exception)
            //    {

            //        await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
            //        return;
            //    }
            //}

        });
        private ObservableCollection<Cartype> _carstypelist = new ObservableCollection<Cartype>();
        public ObservableCollection<Cartype> CarsTypeList
        {
            get
            {
                return _carstypelist;
            }
            set
            {
                _carstypelist = value;
                OnPropertyChanged(nameof(CarsTypeList));
            }
        }

        public List<string> TypeCarName { get; private set; }

        private async void AddToCarTypeList(params Cartype[] cartypes)
        {
            UserService CarType = new UserService();

            {
                CarsTypeList = await CarType.GetCarstype();

            }

            foreach (Cartype typecar in cartypes)
            {

                // CarsTypeList.Add(typecar);
                // TypeCarName = CarsTypeList.Select(o => o.name).ToList();
            }

            CarsTypeList = CarsTypeList;
        }


        //private void PopAlert(bool x)
        //{
        //    //    PopupNavigation.Instance.PushAsync(new TrueRegister());
        //    return;
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
