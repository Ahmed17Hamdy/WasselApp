using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using WasselApp.Services;
using WasselApp.Views.CarsPages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WasselApp.Helpers;
using Rg.Plugins.Popup.Services;
using WasselApp.Views.Popups;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Com.OneSignal;
using Plugin.Multilingual;

namespace WasselApp.Views.Panels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverPanel : ContentPage
    {
        private MediaFile ProfilePic, passportnumber, DriverLicImg;


        UserService userService = new UserService();

        ObservableCollection<Cartype> ResBack;
        private List<string> _carstype;
        private int uid;

        public List<string> CarsType
        {
            get
            {
                return _carstype;
            }
            set
            {
                _carstype = value;
                OnPropertyChanged();
            }
        }
        public DriverPanel()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            // CarsTypedata();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
          //  NatoionalEntry.Text = Settings.ResidentName;
        }
        private bool AllNeeded()
        {
            if (Settings.LastLat == "" || Settings.LastLng == "")
            {
                Active.IsRunning = false;
                DisplayAlert(AppResources.Error, AppResources.Location, AppResources.Ok);
                CrossPermissions.Current.OpenAppSettings();
                return false;
            }
            else if (ProfilePic == null || LiecenceImg == null || passportnumber == null)
            {
                Active.IsRunning = false;
                DisplayAlert(AppResources.Error, AppResources.AddImages, AppResources.Ok);
                return false;
            }
            else if (Settings.CarModelID == string.Empty || Settings.cartype.ToString() == string.Empty || Settings.Type == 0)
            {
                Active.IsRunning = false;
                DisplayAlert(AppResources.Error, AppResources.ChooseCarType, AppResources.Ok);
                PopupNavigation.Instance.PushAsync(new CartypePage());
                return false;
            }
            else if (Settings.Residentid==string.Empty)
            {
                Active.IsRunning = false;
                DisplayAlert(AppResources.Error, AppResources.ChooseResidence, AppResources.Ok);
                PopupNavigation.Instance.PushAsync(new ResidencesListView());
                return false;
            }
            return true;
        }
        //public async void CarsTypedata()
        //{
        //    UserService ser = new UserService();
        //    ResBack = await ser.GetCarstype();
        //    foreach (var item in ResBack)
        //    {
        //        CarTypePicker.Items.Add(item.name);

        //        CarsType = ResBack.Select(o => o.name).ToList();
        //    }
        //    CarsType = CarsType;
        //    // cartypelist.ItemsSource = ResBack.Select(o => o.name).ToList();
        //}
        //private void CarTypeList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var item = ResBack.Where(o => o.name == CarTypePicker.SelectedItem.ToString()).SingleOrDefault();
        //    CarModelPicker.ItemsSource = item.carmodals.Select(o => o.name).ToList();
        //}
        private void login_Tapped(object sender, EventArgs e)
        {
            lgnlbl.TextColor = Color.Blue;
            rgslbl.TextColor = Color.Gray;
            loginlbl.IsVisible = false;
            Registerlbl.IsVisible = true ;
            RegisterPanel.IsVisible = false;
            rgstimg.IsVisible = false;
            LoginPanel.IsVisible = true;
            lgnimg.IsVisible = true;
        }
        private void registerdriver_Tapped(object sender, EventArgs e)
        {
            rgslbl.TextColor = Color.Blue;
            lgnlbl.TextColor = Color.Gray;
            RegisterPanel.IsVisible = true;
            LoginPanel.IsVisible = false;
            lgnimg.IsVisible = false;
            loginlbl.IsVisible = true;
            Registerlbl.IsVisible = false;
            rgstimg.IsVisible = true;
        }

        //private void CarmodelList_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}
        private void IdsAvailable(string userID, string pushToken)
        {
            Settings.LastSignalID = pushToken;
            Settings.UserFirebaseToken = userID;

        }
        private void GetFirbasetoken()
        {
            OneSignal.Current.IdsAvailable(IdsAvailable);
        }
        private async void DriverRegisterCommand(object sender, EventArgs e)
        {
            Active.IsRunning = true;
            var location = await Geolocation.GetLocationAsync();
            Settings.LastLat = location.Latitude.ToString();
            Settings.LastLng = location.Longitude.ToString();
            var device = DeviceInfo.Model;
            if (AllNeeded() == true)
            {
                GetFirbasetoken();
                UserService userService = new UserService();
                DriverUser _driveruser = new DriverUser
                {
                    email = EmailEntry.Text,
                    name = NameEntry.Text,
                    phone = PhoneEntry.Text,
                    password = passwordEntry.Text,
                    confirmpass = confirmpassEntry.Text,
                    country = CountryEntry.Text,
                    city = CityEntry.Text,
                    firebase_token = Settings.UserFirebaseToken,
                    nationality = nationalityEntry.Text,
                    national = Settings.Residentid.ToString(),
                    device_id = Settings.LastSignalID,
                    age = AgeEntry.Text,
                    carmodal = Settings.CarModelID,
                    cartype = Settings.cartype.ToString(),
                    //denominationnumber = denominationnumberimg,
                    //passportnumber = passportnumber.Text,
                    carnumber = CarNumberEntry.Text,
                    idnumber = IdNumberEntry.Text,
                    type = Settings.Type.ToString(),
                    load = loadEntry.Text,
                    lat = Settings.LastLat,
                    lng = Settings.LastLng
                };

                StringContent name = new StringContent(_driveruser.name);
                StringContent phone = new StringContent(_driveruser.phone);
                StringContent email = new StringContent(_driveruser.email);
                StringContent password = new StringContent(_driveruser.password);
                StringContent confirmpass = new StringContent(_driveruser.confirmpass);
                StringContent country = new StringContent(_driveruser.country);
                StringContent city = new StringContent(_driveruser.city);
                StringContent cartype = new StringContent(_driveruser.cartype);
                StringContent carmodal = new StringContent(_driveruser.carmodal);
                StringContent carnumber = new StringContent(_driveruser.carnumber);
                StringContent device_id = new StringContent(_driveruser.device_id);
                StringContent age = new StringContent(_driveruser.age);
                StringContent idnumber = new StringContent(_driveruser.idnumber);
                StringContent firebase_token = new StringContent(_driveruser.firebase_token);
                StringContent national = new StringContent(_driveruser.national);
                StringContent nationality = new StringContent(_driveruser.nationality);
                StringContent load = new StringContent(_driveruser.load);
                StringContent type = new StringContent(_driveruser.type);
                StringContent lat = new StringContent(_driveruser.lat);
                StringContent lng = new StringContent(_driveruser.lng);
                var content = new MultipartFormDataContent();

                content.Add(name, "name");
                content.Add(email, "email");
                content.Add(phone, "phone");
                content.Add(password, "password");
                content.Add(confirmpass, "confirmpass");
                content.Add(country, "country");
                content.Add(city, "city");
                content.Add(cartype, "cartype");
                content.Add(carmodal, "carmodal");
                content.Add(carnumber, "carnumber");
                content.Add(device_id, "device_id");
                content.Add(age, "age");
                content.Add(idnumber, "idnumber");
                content.Add(firebase_token, "firebase_token");
                content.Add(national, "national");
                content.Add(nationality, "nationality");
                content.Add(load, "load");
                content.Add(type, "type");
                content.Add(lat, "lat");
                content.Add(lng, "lng");
                content.Add(new StreamContent(DriverLicImg.GetStream()), "denominationnumber", $"{DriverLicImg.Path}");
                content.Add(new StreamContent(ProfilePic.GetStream()), "image", $"{ProfilePic.Path}");
                content.Add(new StreamContent(passportnumber.GetStream()), "passportnumber", $"{passportnumber.Path}");
                //content.Add(new StreamContent(_mediafile.GetStream()), "images", $"{_mediafile.Path}");
                HttpClient httpClient = new HttpClient();
                try
                {
                    var httpResponseMessage = await httpClient.PostAsync("https://waselksa.alsalil.net/api/register",
                        content);
                    var serverResponse = httpResponseMessage.Content.ReadAsStringAsync().Result.ToString();
                    if (serverResponse == "false")
                    {
                        Active.IsRunning = false;
                        await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                    }
                    else
                    {
                        try
                        {
                            try
                            {
                                Active.IsRunning = false;
                                var JsonResponse = JsonConvert.DeserializeObject<Response<string, Models.DriverUser>>(serverResponse);
                                if (JsonResponse.success == true)
                                {
                                    Settings.LastUsedDriverID = JsonResponse.message.id;
                                    Settings.LastUsedEmail = JsonResponse.message.email;
                                    Settings.LastUserStatus = "0";
                                    Settings.ProfileUser = "2";
                                    Settings.ProfileName = JsonResponse.message.name;
                                    Settings.CarLat = Convert.ToDouble(JsonResponse.message.lat);
                                    Settings.CarLng = Convert.ToDouble(JsonResponse.message.lng);
                                    await PopupNavigation.Instance.PushAsync(new RegisterPopup(JsonResponse.data));
                                    Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new MainTabbedPage());
                                }
                                
                            }
                            catch 
                            {
                                var JsonResponse = JsonConvert.DeserializeObject<RegisterResponse>(serverResponse);
                                if (JsonResponse.success == false)
                                {
                                    Active.IsRunning = false;
                                    await PopupNavigation.Instance.PushAsync(new RegisterPopup(JsonResponse.data));
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Active.IsRunning = false;
                            await PopupNavigation.Instance.PushAsync(new ConnectionPopup());
                            return;
                        }
                       
                    }

                }
                catch (Exception)
                {
                    await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                }

            }


        }

        private async void Cartypebtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new CartypePage());
        }
        private async void ProfileImg_Clicked(object sender, EventArgs e)
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }
            if (storageStatus == PermissionStatus.Granted)
            {
                ProfilePic = await CrossMedia.Current.PickPhotoAsync();
                if (ProfilePic == null)
                    return;
                ProfImgSource.Source = ImageSource.FromStream(() =>
                {
                    return ProfilePic.GetStream();
                });
            }
            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionDetails, AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }
        private async  void NatoionalEntry_Clicked(object sender, EventArgs e)
        {
          //    nationalpicker.Focus();
              await PopupNavigation.Instance.PushAsync(new ResidencesListView());
          
        }
        private void Nationalpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;            
            Resident selectedItem = picker.SelectedItem as Resident;
            uid = selectedItem.id;
            NatoionalEntry.Text = selectedItem.name;
        }
        private async  void Recovery_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoveryPages(), true);
        }
        private async void LiecenceImg_Clicked(object sender, EventArgs e)
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }
            if (storageStatus == PermissionStatus.Granted)
            {
                DriverLicImg = await CrossMedia.Current.PickPhotoAsync();
                if (DriverLicImg == null)
                    return;
                LiecenceImgSource.Source = ImageSource.FromStream(() =>
                {
                    return DriverLicImg.GetStream();
                });
            }
            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionDetails, AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }
        private async void PassPortImg_Clicked(object sender, EventArgs e)
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }
            if (storageStatus == PermissionStatus.Granted)
            {
                passportnumber = await CrossMedia.Current.PickPhotoAsync();
                if (passportnumber == null)
                    return;
                PassPortImgSource.Source = ImageSource.FromStream(() =>
                {
                    return passportnumber.GetStream();
                });
            }
            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionDetails, AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }

    }
}