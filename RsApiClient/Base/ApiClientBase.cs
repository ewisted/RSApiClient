using Microsoft.Extensions.Options;
using RSApiClient.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSApiClient.Base
{
    public abstract class ApiClientBase
    {
        protected readonly HttpClient _httpClient;
        protected readonly IOptions<RSClientOptions> _options;

        protected ApiClientBase(HttpClient httpClient, IOptions<RSClientOptions> options, string moduleToken)
        {
            _httpClient = httpClient;
            _options = options;
			ModuleToken = moduleToken;
        }

		protected string ModuleToken { get; set; }

		protected async Task<T> SendRequestAsync<T>(HttpMethod method, string queryString, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default, int attempt = 1)
        {
			HttpRequestMessage request = new HttpRequestMessage(method, queryString);

			HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
			response.EnsureSuccessStatusCode();

			T result = await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream(), options, cancellationToken) ?? throw new JsonException("Deserialization result was null");

			return result;
		}
    }
}
