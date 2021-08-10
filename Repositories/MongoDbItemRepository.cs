using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories

{

    public class MongoDbItemRepository : ItemRepository
    {
    
       private const string dbname="catalog";
       private const string collectionname="items";

       private readonly IMongoCollection<Item> itemcollection;

       private readonly FilterDefinitionBuilder<Item> filterBuilder=Builders<Item>.Filter;

       public MongoDbItemRepository(IMongoClient mongoClient){
         IMongoDatabase db=mongoClient.GetDatabase(dbname);
         itemcollection=db.GetCollection<Item>(collectionname);
       }


    
            //async is covert void to Task

        
        public async Task CreateItemAsync(Item item)
        {
           await itemcollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter=filterBuilder.Eq(item=>item.Id,id);
            await itemcollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter=filterBuilder.Eq(item=>item.Id,id);

            return await itemcollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemcollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter=filterBuilder.Eq(exitem=>exitem.Id,item.Id);
           await itemcollection.ReplaceOneAsync(filter, item);
        }
    }

}