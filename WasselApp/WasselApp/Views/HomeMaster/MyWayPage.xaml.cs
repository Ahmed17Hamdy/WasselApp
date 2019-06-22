using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using WasselApp.Models;
using WasselApp.Helpers;
using WasselApp.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Connectivity;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.Multilingual;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.ObjectModel;

namespace WasselApp.Views.HomeMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyWayPage : ContentPage
    {
        public MyWayPage()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                  : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            GetLocation();
        }
        private async void GetLocation()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (locationStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                locationStatus = results[Permission.Location];
            }
            if (locationStatus == PermissionStatus.Granted)
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMilliseconds(5000));
                    var location = await Geolocation.GetLocationAsync(request);
                    MainMap.MapRegion = MapSpan.FromCenterAndRadius(
                            new Position(location.Longitude, location.Longitude), Distance.FromKilometers(50));
                }
                catch (FeatureNotEnabledException)
                {
                    await DisplayAlert(AppResources.Alert, AppResources.LocationEnabled, AppResources.Ok);
                }
            }
            else
            {
                await DisplayAlert(AppResources.PermissionsDenied, AppResources.PermissionLocationDetails,
                    AppResources.Ok);
                //On iOS you may want to send your user to the settings screen.
                CrossPermissions.Current.OpenAppSettings();
            }
        }
        private async void MainMap_PinSelected(object sender, TKGenericEventArgs<TKCustomMapPin> e)
        {
            var _Carorder = MainMap.SelectedPin as Car;
            await PopupNavigation.Instance.PushAsync(new CarDetailsPage(_Carorder));
        }

        private async void Searchbtn_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Active.IsVisible = true;
                Active.IsRunning = true;
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();

                    values.Add("addressfrom", FromEntry.Text);
                    values.Add("addressto", ToEntry.Text);

                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/location", new StringContent(content,
                            Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        try
                        {
                            var JsonResponse = JsonConvert.DeserializeObject<Driversback>(serverResponse);
                            if (JsonResponse.success == false)
                            {
                                Active.IsVisible = false;
                                Active.IsRunning = false;
                                await DisplayAlert(AppResources.Alert, JsonResponse.data, AppResources.Ok);
                            }
                       
                        }
                        catch
                        {
                            var Req = JsonConvert.DeserializeObject<RootObject>
                         (serverResponse);
                            if (Req.success == true)
                            {
                                Active.IsVisible = false;
                                Active.IsRunning = false;
                             var   ShippingCars = new ObservableCollection<Car>(Req.message);
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
                                    if (item.Order != null)
                                    {
                                        if (item.Order.weight != null)
                                        {
                                            if (Convert.ToDouble(item.Order.weight) > (0.9 * Convert.ToDouble(item.Member.load)) && item.cartypename == "دينا")
                                            {
                                                item.carmodalname = "dinared";
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
                                MainMap.Pins = ShippingCars;
                            }
                        }
                       
                    }
                    catch (Exception)
                    {
                        Active.IsVisible = false;
                        Active.IsRunning = false;
                        await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                    }
                }
            }
            else
            {
                await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
            }
        }
    }
}