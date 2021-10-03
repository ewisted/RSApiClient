using RSApiClient.Base;
using RSApiClient.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RSApiClient.Extensions.DependencyInjection;

namespace RSApiClient.Hiscores
{
    public abstract class HiscoresApiClientBase : ApiClientBase
    {
        protected HiscoresApiClientBase(HttpClient httpClient, IOptions<RSClientOptions> options) : base(httpClient, options) { }


        
    }
}
