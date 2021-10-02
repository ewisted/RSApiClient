using System.Web;

namespace RSApiClient.Endpoints
{
    public static class EndpointUtils
    {
        public static string GetEncodedQueryUrl(string queryTemplate, params object[] args)
        {
            string?[] encodedArgs = args.Select(arg => HttpUtility.UrlEncode(arg.ToString())).ToArray();
            string encodedString = string.Format(queryTemplate, encodedArgs);
            return encodedString;
        }
    }
}
