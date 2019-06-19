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
using Com.OneSignal;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace WasselApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public Cartype cartype { get; set; }

        public UserViewModel()
        {
         //     ResidentsList = GetResidents();
            residents = new List<Resident>()
            {
            new Resident(){id =0, name=AppResources.resident},
            new Resident(){id=1,name=AppResources.Saudi}

            };
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
        public List<Resident> ResidentsList { get; set; }
        private Resident _selectedresident;
        public Resident SelectedResident
        {
            get { return _selectedresident; }
            set
            {
                if (_selectedresident != value)
                {
                    SetProperty(ref _selectedresident, value);
                }
            }
        }
       
        private void SetProperty(ref Resident selectedresident, Resident value)
        {
            throw new NotImplementedException();
        }

        public  List<Resident> residents { get; set; }
        public List<Resident> GetResidents()
        {
             residents = new List<Resident>()
            {
            new Resident(){id =0, name=AppResources.resident},
            new Resident(){id=1,name=AppResources.Saudi}

            };
            return residents;
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
        private void GetFirbasetoken()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }
        private async void GetLocation()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();
                Settings.LastLat = location.Latitude.ToString();
                Settings.LastLng = location.Longitude.ToString();
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup(y));
                //await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionLocationDetails,
                //    AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }
        public ICommand UserRegisterCommand => new Command(async () =>
        {
            GetFirbasetoken();
            
            GetLocation();
            if (email!=null && name!= null && password!= null && confirmpass!= null )
            {
                IsRunning = true;
                GetLocation();
                
                var device = DeviceInfo.Model;
               
                User _userReg = new User
                {
                    email = email,
                    name = name,
                    password = password,
                    confirmpass = confirmpass,
                    firebase_token = Settings.UserFirebaseToken,
                    device_id = Settings.LastSignalID,
                    lat = Settings.LastLat,
                    lng = Settings.LastLng

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
                            {
                                IsRunning = false;
                                await PopupNavigation.Instance.PushAsync(new RegisterPopup(JsonResponse.data));
                            }
                        }
                        catch
                        {
                            var JsonResponser = JsonConvert.DeserializeObject<Response<string, User>>(ResBack);
                            if (JsonResponser.success == true)
                            {
                                IsRunning = false;
                                await PopupNavigation.Instance.PushAsync(new RegisterPopup(JsonResponser.data));
                                Settings.LastUsedID = JsonResponser.message.id;
                                Settings.LastUsedEmail = JsonResponser.message.email;
                                Settings.ProfileName = JsonResponser.message.name;
                                Settings.ProfileUser = "1";
                                Settings.CarLat = Convert.ToDouble(JsonResponser.message.lat);
                                Settings.CarLng = Convert.ToDouble(JsonResponser.message.lng);
                                Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new HomePage());
                            }
                        }
                    }
                    catch (Exception)
                    {
                        var JsonResponse = JsonConvert.DeserializeObject<RegisterResponse>(ResBack);
                        await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
                        return;
                    }
                }
            }
            else if (email == null || name==null || password==null || confirmpass==null )
            {
              
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup(x));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup(y));
            }

        });
        public ICommand LoginCommand => new Command(async () =>
        {
            GetFirbasetoken();
            if (email != null  && password != null )
            {
                User user = new User
                {
                    name = name,
                    email = email,
                    password = password,
                    confirmpass = confirmpass,
                    firebase_token = Settings.UserFirebaseToken,
                    device_id = Settings.LastSignalID,
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
                            var JsonResponse = JsonConvert.DeserializeObject<Response<string, User>>(ResBack);
                            if (JsonResponse.success == true)
                            {
                                IsRunning = false;
                                var _userID = JsonResponse.message.id;

                                Settings.LastUsedID = _userID;
                                Settings.LastUsedEmail = JsonResponse.message.email;
                                Settings.ProfileName = JsonResponse.message.name;
                                Settings.ProfileUser = "1";
                                Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new HomePage());
                                await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
                            }
                                
                        }
                        catch
                        {
                            
                            var JsonResponse = JsonConvert.DeserializeObject<LoginResponse>(ResBack);
                            if (JsonResponse.success == false)
                            {
                                IsRunning = false;
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
            }
            else if (email == null || name == null || password == null )
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup(x));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
            }
        });
        public ICommand DriverLoginCommand => new Command(async () =>
        {
            GetFirbasetoken();
            if (email != null && password != null)
            {
                User user = new User
                {
                    name = name,
                    email = email,
                    password = password,
                    confirmpass = confirmpass,
                    firebase_token = Settings.UserFirebaseToken,
                    device_id = Settings.LastSignalID,
                };
                var ResBack = await userService.DriverLoginCommandAsync(user);
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
                            var JsonResponse = JsonConvert.DeserializeObject<Response<string, DriverUser>>(ResBack);
                            if (JsonResponse.success == true)
                            {
                                IsRunning = false;
                                var _userID = JsonResponse.message.id;

                                Settings.LastUsedDriverID = _userID;
                                Settings.LastUsedEmail = JsonResponse.message.email;
                                Settings.ProfileName = JsonResponse.message.name;
                                Settings.LastUserStatus = JsonResponse.message.status.ToString();
                                Settings.ProfileUser = "2";
                                await PopupNavigation.Instance.PushAsync(new LoginPopup(JsonResponse.data));
                                Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new MainTabbedPage());
                            }                                                                              
                        }
                        catch
                        {
                            var JsonResponse = JsonConvert.DeserializeObject<LoginResponse>(ResBack);
                            if (JsonResponse.success == false)
                            {
                                IsRunning = false;
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
            }
            else if (email == null || name == null || password == null)
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup(x));
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
            }
        });
        private ObservableCollection<Cartype> _carstypelist = new ObservableCollection<Cartype>();
        private Resident myProperty;
        private object x;
        private int y;

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
