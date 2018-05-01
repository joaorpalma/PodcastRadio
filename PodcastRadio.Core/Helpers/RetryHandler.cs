using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PodcastRadio.Core.Helpers
{
    public class RetryHandler : DelegatingHandler
    {
        private const int MaxRetries = 3;
        private readonly TimeSpan RetryTimeout = TimeSpan.FromSeconds(1);

        public RetryHandler(HttpMessageHandler innerHandler) : base(innerHandler) {}

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            for (int i = 0; i < MaxRetries; i++)
            {
                var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode) 
                    return response;

                await Task.Delay(RetryTimeout, cancellationToken).ConfigureAwait(false);
            }

            return null;
        }
    }
}

// https://stackoverflow.com/questions/19260060/retrying-httpclient-unsuccessful-requests