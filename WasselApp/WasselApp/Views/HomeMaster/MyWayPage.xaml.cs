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
            GetLocation();
        }
        private async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMilliseconds(5000));
            var location = await Geolocation.GetLocationAsync(request);
            MainMap.MapRegion = MapSpan.FromCenterAndRadius(
                    new Position(location.Longitude, location.Longitude), Distance.FromKilometers(50));
        }
        private async void MainMap_PinSelected(object sender, TKGenericEventArgs<TKCustomMapPin> e)
        {
            var _Carorder = MainMap.SelectedPin as Car;
            await PopupNavigation.Instance.PushAsync(new CarDetailsPage(_Carorder));
        }

        private void Searchbtn_Clicked(object sender, EventArgs e)
        {
            //if (CrossConnectivity.Current.IsConnected)
            //{
            //    Active.IsVisible = true;
            //    Active.IsRunning = true;
            //    using (var client = new HttpClient())
            //    {
            //        Dictionary<string, string> values = new Dictionary<string, string>();

            //        values.Add("addressfrom", FromEntry.Text);
            //        values.Add("addressto", ToEntry.Text);

            //        string content = JsonConvert.SerializeObject(values);
            //        try
            //        {
            //            var response = await client.PostAsync("https://waselksa.alsalil.net/api/location", new StringContent(content,
            //                Encoding.UTF8, "text/json"));
            //            var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
            //            var JsonResponse = JsonConvert.DeserializeObject<Response<string, string>>(serverResponse);
            //            if (JsonResponse.success == true)
            //            {
            //                Active.IsVisible = false;
            //                Active.IsRunning = false;
            //                await DisplayAlert(AppResources.Alert, JsonResponse.message, AppResources.Ok);
            //            }
            //            else
            //            {
            //                Active.IsVisible = false;
            //                Active.IsRunning = false;
            //                await DisplayAlert(AppResources.Alert, JsonResponse.message, AppResources.Ok);
            //            }

            //        }
            //        catch (Exception)
            //        {
            //            Active.IsVisible = false;
            //            Active.IsRunning = false;
            //            await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
            //        }
            //    }
            //}
            //else
            //{
            //    await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
            //}
        }
    }
}