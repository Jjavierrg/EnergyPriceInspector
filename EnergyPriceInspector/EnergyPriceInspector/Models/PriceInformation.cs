namespace EnergyPriceInspector.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PriceInformation
    {
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public GeoLocation GeoLocation { get; set; }
    }
}
