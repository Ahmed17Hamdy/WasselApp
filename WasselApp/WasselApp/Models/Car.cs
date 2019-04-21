using System;
using System.Collections.Generic;
using System.Text;
using TK.CustomMap;

namespace WasselApp.Models
{
    public class Car : TKCustomMapPin
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int carorder { get; set; }
        public string carnumber { get; set; }
        public int cartype { get; set; }
        public string cartypename { get; set; }
        public int carmodal { get; set; }
        public string carmodalname { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public int type { get; set; }
        public List<Cartype> cartypes { get; set; }

    }
}
