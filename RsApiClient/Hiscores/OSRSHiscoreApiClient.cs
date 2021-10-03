using RSApiClient.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RSApiClient.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores
{
    public class OSRSHiscoreApiClient : HiscoresApiClientBase
    {
        public OSRSHiscoreApiClient(HttpClient httpClient, IOptions<RSClientOptions> options) : base(httpClient, options) { }

        public async Task GetHiscoresLite(string playerName)
        {
            string query = EndpointUtils.GetEncodedQueryUrl(EndpointUtils.Hiscores_GetHiscoresLiteQueryTemplate, playerName);
            HttpResponseMessage response = await _httpClient.GetAsync(query);
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync() ?? throw new Exception("Response content was empty");
        }
    }
}
