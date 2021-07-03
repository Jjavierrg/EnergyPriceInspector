using System.Net.Http;

namespace EnergyPriceInspector.ApiClient
{
    public interface IApiClientFactory
    {
        HttpClient GetApiClient();
    }
}