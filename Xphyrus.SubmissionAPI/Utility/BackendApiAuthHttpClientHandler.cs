using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Xphyrus.SubmissionAPI.Utility
{
    public class BackendApiAuthHttpClientHandler: DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BackendApiAuthHttpClientHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }

    }
}
