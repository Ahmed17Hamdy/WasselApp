using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WasselApp.Views.HomeMaster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallUsPage : ContentPage
    {
        public CallUsPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Active.IsVisible = true;
                Active.IsRunning = true;
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();

                    values.Add("name", nameentry.Text);
                    values.Add("email", EmailEntry.Text);
                    values.Add("password", PhoneEntry.Text);
                    values.Add("confirmpass", SubjectEntry.Text);
                    values.Add("firebase_token", MessageEntry.Text);
                  
                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/contactus", new StringContent(content,
                            Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        var JsonResponse = JsonConvert.DeserializeObject<Response<string, string>>(serverResponse);
                        if (JsonResponse.success == true)
                        {
                            Active.IsVisible = false;
                            Active.IsRunning = false;
                            await DisplayAlert(AppResources.Alert, JsonResponse.message, AppResources.Ok);
                        }
                        else
                        {
                            Active.IsVisible = false;
                            Active.IsRunning = false;
                            await DisplayAlert(AppResources.Alert, JsonResponse.message, AppResources.Ok);
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