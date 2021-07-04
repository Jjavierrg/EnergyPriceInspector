using EnergyPriceInspector.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyPriceInspector.Services
{
    public interface IEnergyService
    {
        Task<DashboardData> GetDashboardAsync(GeoLocation geoLocation);
        Task<IEnumerable<GeoLocation>> GetGeolocationsAsync();
        Task<GeoLocation> GetGeolocationsFromIdAsync(int geolocationId);
        Task<IEnumerable<PriceInformation>> GetPricesAsync(int? geoId = null, DateTime? fromDate = null, DateTime? toDate = null);
        Task<IEnumerable<PriceInformation>> GetPricesAsync(IEnumerable<int> geoIds, DateTime? fromDate = null, DateTime? toDate = null);
    }
}