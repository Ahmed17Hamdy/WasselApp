using Newtonsoft.Json;
using System;
using WasselApp.Models;
using WasselApp.Helpers;
using WasselApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Multilingual;

namespace WasselApp.Views.Panels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecoveryPagesForUsers : ContentPage
    {
        public RecoveryPagesForUsers()
        {
            InitializeComponent();
            FlowDirection = (Settings.LastUserGravity == "Arabic") ? FlowDirection.RightToLeft
                 : FlowDirection.LeftToRight;
            AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
        }
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Activ.IsRunning = true;
            UserService userService = new UserService();
            string email = EntryEmail.Text;
            var ResBack = await userService.UserBackupEmail(email);
            if (ResBack == "False")
            {
                Activ.IsRunning = false;
                await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
            }
            else
            {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                bool checker = false;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
                try
                {
                    Activ.IsRunning = false;
                    var JsonResponse = JsonConvert.DeserializeObject<Response<string, string>>(ResBack);
                    if (JsonResponse.success == true)
                    {
                        checker = true;
                        await DisplayAlert(AppResources.Alert, AppResources.ConfirmSentCode, AppResources.Ok);
                        ForgetPassWordgrid.IsVisible = false;
                        Activegrid.IsVisible = true;
                        // Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new ActiveCode(EntryEmail.Text));
                    }
                    else
                    {
                        Activ.IsRunning = false;
                        await DisplayAlert(AppResources.Alert, AppResources.WrongEmail, AppResources.Ok);
                        Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new UserPanel());
                        return;
                    }

                }
                catch (Exception)
                {
                    Activ.IsRunning = false;
                    var JsonResponse = JsonConvert.DeserializeObject<Response<string, string>>(ResBack);
                    Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new UserPanel());
                    await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                    return;
                }

            }
        }

        private async void Activebtn_Clicked(object sender, EventArgs e)
        {
            Activ.IsRunning = true;
            if (EntryCode.Text != "")
            {
                UserService ser = new UserService();
                var resback = await ser.UserCodeVerfication(EntryEmail.Text, EntryCode.Text);
                var mess = JsonConvert.DeserializeObject<Response<string, string>>(resback);
                if (mess.success == true)
                {
                    Activ.IsRunning = false;
                    ForgetPassWordgrid.IsVisible = false;
                    Activegrid.IsVisible = false;
                    ChangePassWordgrid.IsVisible = true;
                    //    await Navigation.PushModalAsync(new NewPassword());
                }
                else await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                Activ.IsRunning = false;
            }
            else
            {
                await DisplayAlert(AppResources.Alert, AppResources.EnterVerifyCode, AppResources.Ok);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Activ.IsRunning = true;

            UserService ser = new UserService();
            var response = await ser.UserResetPassword(EntryPass.Text);
            var mess = JsonConvert.DeserializeObject<Response<string, string>>(response);
            if (mess.success == true)
            {
                Activ.IsRunning = false;
                await Navigation.PushModalAsync(new DriverPanel());
            }
            else
            {
                await DisplayAlert(AppResources.Error, AppResources.ErrorMessage, AppResources.Ok);
                Activ.IsRunning = false;
            }
        }
    }
}