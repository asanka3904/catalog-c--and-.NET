using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
     public interface ItemRepository
    {

        
        // Item GetItem(Guid id);
        // IEnumerable<Item> GetItems();

        // void CreateItem(Item item);

        // void UpdateItem(Item item);

        // void DeleteItem(Guid id);

        //add Task to eveyone to async
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();

        Task CreateItemAsync(Item item);

        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}