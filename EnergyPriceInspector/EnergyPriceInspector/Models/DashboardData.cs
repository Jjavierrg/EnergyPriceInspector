namespace EnergyPriceInspector.Models
{
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
        public PriceInformation MaxPrice => Prices?.OrderByDescending(x => x.Price).First();
        public PriceInformation MinPrice => Prices?.OrderBy(x => x.Price).First();
        public PriceInformation ActualPrice => Prices?.FirstOrDefault(x => x.Date.Date == DateTime.Today && x.Date.Hour == DateTime.Now.Hour);
    }
}
