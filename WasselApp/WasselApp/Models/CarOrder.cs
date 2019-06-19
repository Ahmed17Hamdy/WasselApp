using System;
using System.Collections.Generic;
using System.Text;
using TK.CustomMap;

namespace WasselApp.Models
{
    public class CarOrder
    {

        public int type { get; set; }

        public int id { get; set; }
        public string ordernumber { get; set; }
        public int owner_id { get; set; }
        public int user_id { get; set; }
        public string ordertype { get; set; }
        public string latfrom { get; set; }
        public string lngfrom { get; set; }
        public string latto { get; set; }
        public string lngto { get; set; }
        public string addressfrom { get; set; }
        public string addressto { get; set; }
        public string deliverydate { get; set; }
        public string desc { get; set; }
        public double? price { get; set; }
        public int? weight { get; set; }
        public int done { get; set; }
        public int? distance { get; set; }
        public int? waiting_hours { get; set; }
        public int status { get; set; }
        public string reason { get; set; }
        public string created_at { get; set; }
    }
    public class DelivaryObject : TKCustomMapPin
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string Img { get; set; }
        public string phone { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string weight { get; set; }
        public string activity { get; set; }
        public string images { get; set; }
        public string images1 { get; set; }
        public string images2 { get; set; }
        public string desc { get; set; }
    }
    public class CheckedCarOrder : CarOrder
        {
        public int order_id { get; set; }
    }
    public class CarOrderReturn
    {
        public int id { get; set; }
        public string ordernumber { get; set; }
        public int user_id { get; set; }
        public int owner_id { get; set; }
        public string ordertype { get; set; }
        public string latfrom { get; set; }
        public string lngfrom { get; set; }
        public string addressfrom { get; set; }
        public string latto { get; set; }
        public string lngto { get; set; }
        public string addressto { get; set; }
        public string deliverydate { get; set; }
        public object desc { get; set; }
        public object price { get; set; }
        public int done { get; set; }
        public int weight { get; set; }
        public int distance { get; set; }
        public int waiting_hours { get; set; }
        public int status { get; set; }
        public object reason { get; set; }
        public string created_at { get; set; }
    }
}
