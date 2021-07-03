namespace EnergyPriceInspector.Services
{
    using EnergyPriceInspector.ApiClient;
    using EnergyPriceInspector.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class EnergyService: IEnergyService
    {
        public EnergyService() {
            ApiClientFactory = DependencyService.Get<IApiClientFactory>() ?? throw new ArgumentNullException("IApiClientFactory");
        }

        private IApiClientFactory ApiClientFactory { get; }

        public async Task<PriceResponse> GetPricesAsync()
        {
            var client = ApiClientFactory.GetApiClient();
            var result = await client.GetAsync("/indicators/10391");
            var jsonResponse = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PriceResponse>(jsonResponse);
        }
    }
}
