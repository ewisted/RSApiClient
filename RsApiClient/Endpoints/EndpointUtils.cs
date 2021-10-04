using System.Web;

namespace RSApiClient.Endpoints
{
    public static class EndpointUtils
    {
        // Grand Exchange Endpoints
        public const string GrandExchange_GetItemByIdQueryTemplate = "m={0}/api/catalogue/detail.json?item={1}";
        public const string GrandExchange_GetItemCatalogueQueryTemplate = "m={0}/api/catalogue/category.json?category={1}";
        public const string GrandExchange_GetItemGraphDataQueryTemplate = "m={0}/api/graph/{1}.json";
        public const string GrandExchange_GetItemsQueryTemplate = "m={0}/api/catalogue/items.json?category={1}&alpha={2}&page={3}";

        // Hiscores Endpoints
        public const string Hiscores_GetHiscoresLiteQueryTemplate = "m={0}/api/index_lite.ws?player={1}";

        public static string GetEncodedQueryUrl(string queryTemplate, params object[] args)
        {
            string?[] encodedArgs = args.Select(arg => HttpUtility.UrlEncode(arg.ToString())).ToArray();
            string encodedString = string.Format(queryTemplate, encodedArgs);
            return encodedString;
        }
    }
}
