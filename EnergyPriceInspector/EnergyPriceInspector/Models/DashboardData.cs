namespace EnergyPriceInspector.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DashboardData
    {
        public DashboardData() { }

        public DashboardData(DateTime updatedDate, GeoLocation geoLocation, IEnumerable<PriceInformation> prices)
        {
            UpdatedDate = updatedDate;
            GeoLocation = geoLocation;
            Prices = prices;
        }
        public DateTime UpdatedDate { get; set; }
        public GeoLocation GeoLocation { get; set; }
        public IEnumerable<PriceInformation> Prices { get; set; }

        [JsonIgnore]
        public PriceInformation MaxPrice => Prices?.OrderByDescending(x => x.Price).First();
        [JsonIgnore]
        public PriceInformation MinPrice => Prices?.OrderBy(x => x.Price).First();
        [JsonIgnore]
        public PriceInformation ActualPrice => Prices?.FirstOrDefault(x => x.Date.Date == DateTime.Today && x.Date.Hour == DateTime.Now.Hour);
    }
}
