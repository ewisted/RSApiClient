using System.Web;

namespace RSApiClient.Endpoints
{
    public static class EndpointUtils
    {
        // Grand Exchange Endpoints
        public const string GrandExchange_GetItemByIdQueryTemplate = "catalogue/detail.json?item={0}";
        public const string GrandExchange_GetItemCatalogueQueryTemplate = "catalogue/category.json?category={0}";
        public const string GrandExchange_GetItemGraphDataQueryTemplate = "graph/{0}.json";
        public const string GrandExchange_GetItemsQueryTemplate = "catalogue/items.json?category={0}&alpha={1}&page={2}";

        // Hiscores Endpoints
        public const string Hiscores_GetHiscoresLiteQueryTemplate = "index_lite.ws?player={0}";

        public static string GetEncodedQueryUrl(string queryTemplate, params object[] args)
        {
            string?[] encodedArgs = args.Select(arg => HttpUtility.UrlEncode(arg.ToString())).ToArray();
            string encodedString = string.Format(queryTemplate, encodedArgs);
            return encodedString;
        }
    }
}
