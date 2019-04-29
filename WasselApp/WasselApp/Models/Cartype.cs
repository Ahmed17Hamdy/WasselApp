using System;
using System.Collections.Generic;
using System.Text;

namespace WasselApp.Models
{
    public class Cartype
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int parent { get; set; }
        public List<Carmodal> carmodals { get; set; }
        public List<Carmodal> brickcarmodals { get; set; }
    }
}
