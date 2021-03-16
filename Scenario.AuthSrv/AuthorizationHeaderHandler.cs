using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Scenario.AuthSrv
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthorizationHeaderHandler> _logger;

        public AuthorizationHeaderHandler(IHttpContextAccessor httpContextAccessor, ILogger<AuthorizationHeaderHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage;
            try
            {
                string accessToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization];
                //TODO: why it's not working? await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
                if (string.IsNullOrEmpty(accessToken))
                {
                    _logger.LogWarning("Request with empty access token: {RequestMethod} {RequestUri} {AccessToken}", request.Method, request.RequestUri, accessToken);
                }
                else
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken.Replace("Bearer ", ""));
                }

                _logger.LogInformation("Authorized request is sending: {RequestMethod} {RequestUri}", request.Method, request.RequestUri);

                httpResponseMessage = await base.SendAsync(request, cancellationToken);

                _logger.LogInformation("Authorized request is finished: {RequestMethod} {RequestUri} with {StatusCode}", request.Method, request.RequestUri, httpResponseMessage.StatusCode);

                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);
                throw;
            }
            return httpResponseMessage;
        }
    }
}