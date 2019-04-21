using System;
using System.Collections.Generic;
using System.Text;

namespace WasselApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public string password { get; set; }
        public string confirmpass { get; set; }
        public string firebase_token { get; set; }
        public string device_id { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
