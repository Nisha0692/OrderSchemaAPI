
using System;
using System.Collections.Generic;

namespace OrderSchemaAPI.Common
{
    public class PickupAddressView
    {
        public int Unit { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
    }
}