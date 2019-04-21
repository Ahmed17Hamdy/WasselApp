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

namespace WasselApp.Views.Panels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverPanel : ContentPage
    {
        private MediaFile _mediafile;
        UserService userService = new UserService();

        ObservableCollection<Cartype> ResBack;
        private List<string> _carstype;
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
            CarsTypedata();
        }
        public async void CarsTypedata()
        {
            UserService ser = new UserService();
            ResBack = await ser.GetCarstype();
            foreach (var item in ResBack)
            {
                CarTypePicker.Items.Add(item.name);

                CarsType = ResBack.Select(o => o.name).ToList();
            }
            CarsType = CarsType;
            // cartypelist.ItemsSource = ResBack.Select(o => o.name).ToList();
        }
        private void CarTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = ResBack.Where(o => o.name == CarTypePicker.SelectedItem.ToString()).SingleOrDefault();
            CarModelPicker.ItemsSource = item.carmodals.Select(o => o.name).ToList();
        }
        private void login_Tapped(object sender, EventArgs e)
        {
            lgnlbl.TextColor = Color.Blue;
            rgslbl.TextColor = Color.Gray;
            RegisterPanel.IsVisible = false;
            rgstimg.IsVisible = false;
            LoginPanel.IsVisible = true;
            lgnimg.IsVisible = true;
        }

        private void registerdriver_Tapped(object sender, EventArgs e)
        {
            rgslbl.TextColor = Color.Blue;
            lgnlbl.TextColor = Color.Gray;
            LoginPanel.IsVisible = false;
            lgnimg.IsVisible = false;
            RegisterPanel.IsVisible = true;
            rgstimg.IsVisible = true;
        }

        private void CarmodelList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async void DriverRegisterCommand(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            var device = DeviceInfo.Model;
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
                firebase_token = "35",
                nationality = nationalityEntry.Text,
                national = NatoionalEntry.Text,
                device_id = "111.2225.555",
                age = AgeEntry.Text,
                carmodal = CarModelPicker.SelectedIndex.ToString(),
                cartype = CarTypePicker.SelectedIndex.ToString(),
                //denominationnumber = denominationnumberimg,
                //passportnumber = passportnumber.Text,
                carnumber = CarNumberEntry.Text,
                idnumber = IdNumberEntry.Text,
                type = typeEntry.Text,
                lat = location.Latitude.ToString(),
                lng = location.Longitude.ToString()
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
            content.Add(type, "type");
            content.Add(lat, "lat");
            content.Add(lng, "lng");
            content.Add(new StreamContent(_mediafile.GetStream()), "denominationnumber", $"{_mediafile.Path}");
            content.Add(new StreamContent(_mediafile.GetStream()), "image", $"{_mediafile.Path}");
            content.Add(new StreamContent(_mediafile.GetStream()), "passportnumber", $"{_mediafile.Path}");
            //content.Add(new StreamContent(_mediafile.GetStream()), "images", $"{_mediafile.Path}");
            HttpClient httpClient = new HttpClient();
            try
            {
                var httpResponseMessage = await httpClient.PostAsync("https://waselksa.alsalil.net/api/register", content);
                var serverResponse = httpResponseMessage.Content.ReadAsStringAsync().Result.ToString();
                if (serverResponse == "false")
                {
                    await DisplayAlert("Connection Error", "من فضلك تحقق من الإتصال بالإنترنت", "OK");
                }
                else
                {
                    // bool checker = false;
                    try
                    {
                        //  Activ.IsRunning = false;
                        var JsonResponse = JsonConvert.DeserializeObject<Response<string, Models.DriverUser>>(serverResponse);
                        if (JsonResponse.success == true)
                        {
                            //  checker = true;
                            // PopAlert(checker);
                            Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new MainTabbedPage());
                        }
                        else
                        {
                            //   Activ.IsRunning = false;
                            //  PopAlert(checker);
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        //   Activ.IsRunning = false;
                        var JsonResponse = JsonConvert.DeserializeObject<Response<object, string>>(serverResponse);
                        //   PopAlert(checker);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("خطأ", "خطأ بالإتصال بالإنترنت من فضلك حاول في وقت لاحق !!", "موافق");
            }
        }
        private async void DenominationnumberEntry_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("ليس هناك صور", "لا يوجد صور متاحة", "حسناً");
                return;
            }
            _mediafile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediafile == null)
                return;
            // denominationnumberEntry.Image = _mediafile.Path;
            denominationnumberEntry.Source = ImageSource.FromStream(() =>
            {
                return _mediafile.GetStream();
            });
        }
    }
}