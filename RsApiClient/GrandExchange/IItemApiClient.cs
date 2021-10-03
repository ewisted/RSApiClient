using RSApiClient.GrandExchange.Models;

namespace RSApiClient.GrandExchange
{
    public interface IItemApiClient
    {
        /// <summary>
        /// Gets an item listed on the Grand Exchange by its id.
        /// </summary>
        /// <param name="itemId">The RS item id to query for.</param>
        /// <returns>The item matching the given id.</returns>
        Task<Item> GetItemByIdAsync(int itemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the number of items for each character in the catalogue.
        /// </summary>
        /// <param name="category">The category to get the catalogue for.</param>
        /// <returns></returns>
        Task<CategoryItemCatalogue> GetItemCatalogueAsync(ItemCategory category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a stream of pages of items from the Grand Exchange api. Consumed in an await foreach loop and returns a page of items each iteration.
        /// WARNING: This is a long running operation and should be executed infrequently due to rate limiting and a poor implementation of pagination by Jagex.
        /// A cancellation token is also strongly recommended.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token used to break out of the request loop when cancel is called on the token source.</param>
        /// <returns>An async stream of item pages.</returns>
        IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a single page of items from the Grand Exchange API.
        /// </summary>
        /// <param name="category">An integer representing an item category. Valid values are 0-41 for RS3 and 1 for OSRS.</param>
        /// <param name="character">A lowercase character representing the first character in an item's name. For numerics, use '#'.</param>
        /// <param name="page">An integer representing the page to get for the given category and character.</param>
        /// <returns>A single item page.</returns>
        Task<ItemPage> GetItemPageAsync(ItemCategory category, char character, int page, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the Grand Exchange price trend graph data for the given item id.
        /// </summary>
        /// <param name="itemId">The RS item id to get graph data for.</param>
        /// <returns>An object representing the daily and average trend graphs.</returns>
        Task<ItemGraphData> GetGraphDataForItem(int itemId, CancellationToken cancellationToken = default);
    }
}
