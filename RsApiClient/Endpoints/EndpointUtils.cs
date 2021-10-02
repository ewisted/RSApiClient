using System.Web;

namespace RSApiClient.Endpoints
{
    public static class EndpointUtils
    {
        public static string GetEncodedQueryUrl(string queryTemplate, params object[] args)
        {
            string unencodedString = string.Format(queryTemplate, args);
            var encodedString = HttpUtility.UrlEncode(unencodedString);
            if (string.IsNullOrWhiteSpace(encodedString))
            {
                throw new ArgumentException($"Unable to URL encode the query string: {queryTemplate}");
            }
            return encodedString;
        }
    }
}
