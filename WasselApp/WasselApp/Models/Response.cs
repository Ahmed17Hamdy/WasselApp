using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WasselApp.Models
{
    public class Response<T, I>
    {
        public bool success { get; set; }
        public T data { get; set; }
        public I message { get; set; }
    }
    public class RegisterResponse
    {
        public bool success { get; set; }
        public Data data { get; set; }
        public User message { get; set; }
    }
    public class Data
    {
        public string data { get; set; }
        public List<string> name { get; set; }
        public List<string> email { get; set; }
        public List<string> password { get; set; }
        public List<string> lat { get; set; }
        public List<string> lng { get; set; }
        public List<string> confirmpass { get; set; }
    }
    public class MainResponseMessage
    {
       
        public ObservableCollection<Settinginfo> settings { get; set; }
        public ObservableCollection<Car> cars { get; set; }
        public ObservableCollection<Cartype> cartype { get; set; }
    }
}
