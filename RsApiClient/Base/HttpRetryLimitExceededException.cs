using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Base
{
    public class HttpRetryLimitExceededException : Exception
    {
        public int MaxRetries { get; set; }
        public HttpMethod Method { get; set; }
        public string Query { get; set; }

        public HttpRetryLimitExceededException(int maxRetries, HttpMethod method, string query, Exception innerException)
            : base($"Max retries ({maxRetries}) exceeded for request: {method} {query}", innerException)
        {
            MaxRetries = maxRetries;
            Method = method;
            Query = query;
        }
    }
}
