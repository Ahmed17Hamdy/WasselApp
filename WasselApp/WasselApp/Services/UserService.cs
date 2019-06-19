using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WasselApp.Models;
using WasselApp.Helpers;
namespace WasselApp.Services
{
    public class UserService
    {
        public async Task<string> LoginCommandAsync(User  user)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();

                    values.Add("name", user.name);
                    values.Add("email", user.email);
                    values.Add("password",user.password);
                    values.Add("confirmpass", user.confirmpass);
                    values.Add("firebase_token", user.firebase_token);
                    values.Add("device_id", user.device_id);
                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/userlogin", new StringContent(content, 
                            Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        return serverResponse;
                    }
                    catch (Exception)
                    {
                        return "false";
                    }
                }
            }
            else return "false";
        }
        public async Task<string> DriverLoginCommandAsync(User user)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();

                    values.Add("name", user.name);
                    values.Add("email", user.email);
                    values.Add("password", user.password);
                    values.Add("confirmpass", user.confirmpass);
                    values.Add("firebase_token", user.firebase_token);
                    values.Add("device_id", user.device_id);
                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/login", new StringContent(content,
                            Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        return serverResponse;
                    }
                    catch (Exception)
                    {
                        return "false";
                    }
                }
            }
            else return "false";
        }
        public async Task<string> RegisterAsync(User userReg)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (var client = new HttpClient())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    values.Add("name", userReg.name);
                    values.Add("email", userReg.email);
                    values.Add("password", userReg.password);
                    values.Add("confirmpass", userReg.confirmpass);
                    values.Add("lat", userReg.lat);
                    values.Add("lng", userReg.lng);
                    values.Add("firebase_token", userReg.firebase_token);
                    values.Add("device_id", userReg.device_id);
                    string content = JsonConvert.SerializeObject(values);
                    try
                    {
                        var response = await client.PostAsync("https://waselksa.alsalil.net/api/userregister", 
                            new StringContent(content, Encoding.UTF8, "text/json"));
                        var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                        
                        return serverResponse;
                    }
                    catch (Exception)
                    {
                        return "false";
                    }
                }
            }
            else return "false";
        }
        public async Task<ObservableCollection<Cartype>> GetCarstype()
        {
            var client = new HttpClient();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    var response = await client.GetAsync("https://waselksa.alsalil.net/api/settingindex");
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    var Req = JsonConvert.DeserializeObject<Response<string, MainResponseMessage>>(serverResponse);
                    var Carstype = Req.message.cartype;

                    return Carstype;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else return null;
        }
        public async Task<string> ChangePassword(string OldPass, string NewBassword)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("old_password", OldPass);
                values.Add("new_password", NewBassword);
                values.Add("confirmpassword", NewBassword);                
                values.Add("user_id",Settings.LastUsedID.ToString());

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/changepassword",
                        new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
        public async Task<string> BackupEmail(string _email)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("email", _email);
              //  values.Add("user_hash", Settings.UserHash);

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/forgetpassword",
                        new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
        public async Task<string> UserBackupEmail(string _email)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("email", _email);
                //  values.Add("user_hash", Settings.UserHash);

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/forgetpassworduser",
                        new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
        public async Task<string> CodeVerfication(string mail, string Code)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("email", mail);
                values.Add("forgetcode", Code);
                values.Add("user_hash", Settings.UserHash);

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/activcode", new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
        public async Task<string> UserCodeVerfication(string mail, string Code)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("email", mail);
                values.Add("forgetcode", Code);
                values.Add("user_hash", Settings.UserHash);

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/activcodeuser", new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
        public async Task<string> ResetPassword(string NewBassword)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("new_password", NewBassword);
                values.Add("confirmpassword", NewBassword);
                values.Add("user_hash", Settings.UserHash);

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/rechangepass", new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
        public async Task<string> UserResetPassword(string NewBassword)
        {
            using (var client = new HttpClient())
            {
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("new_password", NewBassword);
                values.Add("confirmpassword", NewBassword);
                values.Add("user_hash", Settings.UserHash);

                string content = JsonConvert.SerializeObject(values);
                try
                {
                    var response = await client.PostAsync("https://waselksa.alsalil.net/api/rechangepassuser", new StringContent(content, Encoding.UTF8, "text/json"));
                    var serverResponse = response.Content.ReadAsStringAsync().Result.ToString();
                    return serverResponse;
                }
                catch (Exception)
                {
                    return "False";
                }
            }
        }
    }
}
