using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TmsUI.Models
{
    public class Load
    {
        public int Id { get; set; }
        public string LoadNumber { get; set; }
        public string Broker { get; set; }
        public string PickUpAddress { get; set; }
        public string DeliveryAddress { get; set; }
    }
}