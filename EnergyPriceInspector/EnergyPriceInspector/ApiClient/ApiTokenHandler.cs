namespace EnergyPriceInspector.ApiClient
{
    using EnergyPriceInspector.Models;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ApiTokenHandler : DelegatingHandler
    {
        public ApiTokenHandler() : this(null) { }
        public ApiTokenHandler(HttpMessageHandler innerHandler) : base(innerHandler ?? new HttpClientHandler()) => Configuration = DependencyService.Get<IConfiguration>() ?? throw new ArgumentNullException("IConfiguration");

        public IConfiguration Configuration { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("Authorization"))
                request.Headers.Add("Authorization", $"Token token={Configuration.ApiToken}");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
