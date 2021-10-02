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
        private readonly HttpClient _httpClient;

        protected ApiClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
            DelayBetweenRetries = TimeSpan.FromSeconds(3);
            MaxRetries = 3;
        }

        /// <summary>
        /// Time to wait before retrying failed requests. Default is 3 seconds.
        /// </summary>
        public TimeSpan DelayBetweenRetries { get; set; }
        /// <summary>
        /// Maximum number of times to retry a failed request. Defaul is 3.
        /// </summary>
        public int MaxRetries { get; set; }

        protected async Task<T> SendRequestAsync<T>(HttpMethod method, string queryString, JsonSerializerOptions? options = null, int attempt = 1)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, queryString);

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                T result = await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream()) ?? throw new JsonException("Deserialization result was null");

                return result;
            }
            catch (Exception ex)
            {
                if (attempt <= MaxRetries)
                {
                    await Task.Delay(DelayBetweenRetries);
                    return await SendRequestAsync<T>(method, queryString, options, attempt + 1);
                }
                else
                {
                    throw new HttpRetryLimitExceededException(MaxRetries, method, $"{_httpClient.BaseAddress}{queryString}", ex);
                }
            }
        }
    }
}
