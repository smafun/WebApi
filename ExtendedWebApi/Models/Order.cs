using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ExtendedWebApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public string Date { get; set; }
        public string ServiceTypes { get; set; }
        public string TxtField { get; set; }
        public int CustomerId { get; set; }
        //public ICollection<ServiceType> ServiceTypes { get; set; }
    }
}
