namespace EnergyPriceInspector.ApiClient
{
    using EnergyPriceInspector.Models;
    using System;
    using System.Net.Http;
    using Xamarin.Forms;

    public class ApiClientFactory: IApiClientFactory
    {
        public ApiClientFactory()
        {
            Configuration = DependencyService.Get<IConfiguration>() ?? throw new ArgumentNullException("IConfiguration");
            var apiTokenHandler = DependencyService.Get<ApiTokenHandler>() ?? throw new ArgumentNullException("ApiTokenHandler");
            var apiRetryHandler = DependencyService.Get<ApiRetryHandler>() ?? throw new ArgumentNullException("ApiRetryHandler");

            apiRetryHandler.InnerHandler = apiTokenHandler;
            ApiClientHandler = apiRetryHandler;
        }

        private IConfiguration Configuration { get; }
        private DelegatingHandler ApiClientHandler { get; }

        public HttpClient GetApiClient() => new HttpClient(ApiClientHandler)
        {
            BaseAddress = new Uri(Configuration.ApiEndpoint)
        };
    }
}
