using System.Collections.Generic;  //to List
using System;
using Catalog.Entities;  //to Item
using System.Linq;

namespace Catalog.Repositories
{
   

    public class InMemItemsRepository : ItemRepository
    {

        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Postion", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "shoes", Price = 15, CreatedDate = DateTimeOffset.UtcNow }
        };


        public IEnumerable<Item> GetItemsAsync()
        {
            return items;
        }


        public Item GetItemAsync(Guid id)
        {

            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItemAsync(Item item)
        {
             items.Add(item);
        }

        public void UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(exitem=>exitem.Id == item.Id);
            items[index]=item;
        }


       

        public void DeleteItemAsync(Guid id)
        {
            var index= items.FindIndex(exitem=>exitem.Id == id);

            items.RemoveAt(index);
        }
    }
}