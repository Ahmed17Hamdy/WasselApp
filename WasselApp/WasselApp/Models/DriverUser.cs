using System;
using System.Collections.Generic;
using System.Text;

namespace WasselApp.Models
{
 public   class DriverUser : User
    {
        public string carnumber { get; set; }
        public string cartype { get; set; }
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
        public string load { get; set; }
        public int status { get; set; }
        public int suspensed { get; set; }
        public string debt { get; set; }
    }
}
