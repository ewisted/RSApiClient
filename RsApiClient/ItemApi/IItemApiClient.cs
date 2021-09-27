using RSApiClient.Models;

namespace RSApiClient.ItemApi
{
    public interface IItemApiClient
    {
        Task<Item> GetItemByIdAsync(int itemId);
        IAsyncEnumerable<ItemPage> GetAllItemsAsync();
        Task<ItemGraphData> GetGraphDataForItem(int itemId);
    }
}
