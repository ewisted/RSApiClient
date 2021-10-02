using RSApiClient.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores
{
    public abstract class HiscoresApiClientBase : ApiClientBase
    {
        protected HiscoresApiClientBase(string baseUrl) : this(new HttpClient() { BaseAddress = new Uri(baseUrl) }) { }
        protected HiscoresApiClientBase(HttpClient httpClient) : base(httpClient) { }


    }
}
