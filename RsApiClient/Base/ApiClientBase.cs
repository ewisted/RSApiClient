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

        protected ApiClientBase(HttpClient httpClient, IOptions<RSClientOptions> options)
        {
            _httpClient = httpClient;
            _options = options;
        }
        protected async Task<T> SendRequestAsync<T>(HttpMethod method, string queryString, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default, int attempt = 1)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, queryString);

                HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();

                T result = await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream(), options, cancellationToken) ?? throw new JsonException("Deserialization result was null");

                return result;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(TaskCanceledException)) throw;
                if (attempt <= _options.Value.MaxRetries)
                {
                    await Task.Delay(_options.Value.DelayBetweenRetries);
                    return await SendRequestAsync<T>(method, queryString, options, cancellationToken, attempt + 1);
                }
                else
                {
                    throw new HttpRetryLimitExceededException(_options.Value.MaxRetries, method, $"{_httpClient.BaseAddress}{queryString}", ex);
                }
            }
        }
    }
}
