namespace EnergyPriceInspector.Services
{
    using EnergyPriceInspector.ApiClient;
    using EnergyPriceInspector.Extensions;
    using EnergyPriceInspector.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class EnergyService : IEnergyService
    {
        public EnergyService()
        {
            ApiClientFactory = DependencyService.Get<IApiClientFactory>() ?? throw new ArgumentNullException("IApiClientFactory");
        }

        private IApiClientFactory ApiClientFactory { get; }

        public Task<IEnumerable<GeoLocation>> GetGeolocationsAsync()
        {
            IEnumerable<GeoLocation> result = new[] {
                new GeoLocation { Id = 8741, Name = Langs.Langs.Peninsula },
                new GeoLocation { Id = 8742, Name = Langs.Langs.Canarias },
                new GeoLocation { Id = 8743, Name = Langs.Langs.Baleares },
                new GeoLocation { Id = 8744, Name = Langs.Langs.Ceuta },
                new GeoLocation { Id = 8745, Name = Langs.Langs.Melilla }
            };

            return Task.FromResult(result);
        }

        public async Task<GeoLocation> GetGeolocationsFromIdAsync(int geolocationId)
        {
            var geoLocations = await GetGeolocationsAsync();
            return geoLocations?.FirstOrDefault(x => x.Id == geolocationId);
        }

        public async Task<DashboardData> GetDashboardAsync(GeoLocation geoLocation)
        {
            geoLocation ??= UserConfiguration.DEFAULT_GEOLOCATION;
            var prices = await GetPricesAsync(geoLocation.Id, DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1));
            return new DashboardData(DateTime.Now, geoLocation, prices);
        }

        public Task<IEnumerable<PriceInformation>> GetPricesAsync(int? geoId = null, DateTime? fromDate = null, DateTime? toDate = null) => GetPricesAsync(geoId.HasValue ? new[] { geoId.Value } : Array.Empty<int>(), fromDate, toDate);

        public async Task<IEnumerable<PriceInformation>> GetPricesAsync(IEnumerable<int> geoIds, DateTime? fromDate = null, DateTime? toDate = null)
        {
            fromDate ??= DateTime.Today;
            toDate ??= DateTime.Today.AddDays(1).AddSeconds(-1);

            var url = $"/indicators/10391?start_date={fromDate.Value:o}&end_date={toDate.Value:o}";
            if (geoIds?.Any() ?? false)
                url += "&" + string.Join('&', geoIds.Select(x => $"geo_ids[]={x}"));

            var client = ApiClientFactory.GetApiClient();
            var httpResponse = await client.GetAsync(url);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            var result = await apiResponse?.indicator?.values.SelectAsync(async x => new PriceInformation { Date = x.datetime, Price = x.value / 1000, GeoLocation = await GetGeolocationsFromIdAsync(x.geo_id) });
            return result ?? Array.Empty<PriceInformation>();
        }
    }
}
