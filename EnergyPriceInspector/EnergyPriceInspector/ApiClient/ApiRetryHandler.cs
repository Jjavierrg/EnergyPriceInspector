namespace EnergyPriceInspector.ApiClient
{
    using EnergyPriceInspector.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class ApiRetryHandler : DelegatingHandler
    {
        public ApiRetryHandler() : this(5) { }
        public ApiRetryHandler(HttpMessageHandler innerHandler) : this(5, 300, innerHandler) { }
        public ApiRetryHandler(int maxRetries) : this(maxRetries, 300) { }
        public ApiRetryHandler(int maxRetries, int milisecondsDelay) : this(maxRetries, milisecondsDelay, null) { }
        public ApiRetryHandler(int maxRetries, int milisecondsDelay, HttpMessageHandler innerHandler) : base(innerHandler ?? new HttpClientHandler())
        {
            MaxRetries = maxRetries;
            MilisecondsDelay = milisecondsDelay;
        }

        private int MaxRetries { get; }
        private int MilisecondsDelay { get; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            for (var i = 0; i < MaxRetries; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                    return response;

                cancellationToken.ThrowIfCancellationRequested();
                if (MilisecondsDelay > 0)
                    await Task.Delay(MilisecondsDelay);
            }

                response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
