namespace RSApiClient.Endpoints
{
    public static class ItemEndpoints
    {
        public const string GetItemByIdQueryTemplate = "catalogue/detail.json?item={0}";
        public const string GetItemGraphDataQueryTemplate = "graph/{0}.json";
        public const string GetItemsQueryTemplate = "catalogue/items.json?category={0}&alpha={1}&page={2}";
    }
}
