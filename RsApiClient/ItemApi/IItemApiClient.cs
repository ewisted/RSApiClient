using RSApiClient.Models;

namespace RSApiClient.ItemApi
{
    public interface IItemApiClient
    {
        /// <summary>
        /// Gets an item listed on the Grand Exchange by its id.
        /// </summary>
        /// <param name="itemId">The RS item id to query for.</param>
        /// <returns>The item matching the given id.</returns>
        Task<Item> GetItemByIdAsync(int itemId);

        /// <summary>
        /// Retrieves a stream of pages of items from the Grand Exchange api. Consumed in an await foreach loop and returns a page of items each iteration.
        /// WARNING: This is a long running operation and should be executed infrequently due to rate limiting and a poor implementation of pagination by Jagex.
        /// A cancellation token is also strongly recommended.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token used to break out of the request loop when cancel is called on the token source.</param>
        /// <returns>An async stream of item pages.</returns>
        IAsyncEnumerable<ItemPage> GetAllItemsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the Grand Exchange price trend graph data for the given item id.
        /// </summary>
        /// <param name="itemId">The RS item id to get graph data for.</param>
        /// <returns>An object representing the daily and average trend graphs.</returns>
        Task<ItemGraphData> GetGraphDataForItem(int itemId);
    }
}
