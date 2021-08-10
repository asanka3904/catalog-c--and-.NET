using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers

{

    //Route https://localhost:5001  /items

 [ApiController]
 [Route("items")]
public class ItemsController: ControllerBase
{
    private readonly ItemRepository repository;

    public ItemsController(ItemRepository repository)
    {
        this.repository = repository;
    }

     
     //GET /items
    [HttpGet]

    public async Task<IEnumerable<ItemDtos>> GetItems()
    {
        var items= (await repository.GetItemsAsync()).Select(item => item.AsDto());
        return items;
    }

    //GET /items /{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDtos>> GetItem(Guid id){
        var item= await repository.GetItemAsync(id);

        if(item is null){
            return NotFound();
        }
        return item.AsDto();
    }

    //POST / items
    [HttpPost]
    public async Task<ActionResult<ItemDtos>> createItem(CreateItemDtos _item)
    {
           Item item=new(){
               Id=Guid.NewGuid(),
               Name=_item.Name,
               Price=_item.Price,
               CreatedDate=DateTimeOffset.UtcNow
           };

          await repository.CreateItemAsync(item);

           return CreatedAtAction(nameof(GetItem),new {id=item.Id},item.AsDto());
    }

    //PUT /items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDtos>> UpdateItem(Guid id,UpdateItemDtos _item){
        
        var existingItem= await repository.GetItemAsync(id);

        if(existingItem is null){
            return NotFound();
        }

         Item updatedItem= existingItem with {
             Name=_item.Name,
             Price=_item.Price,
         };

      await  repository.UpdateItemAsync(updatedItem);

        return NoContent();
    }

    
     //DELETE /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(Guid id){

        var existingItem=await repository.GetItemAsync(id);

        if(existingItem is null){
            return NotFound();
        }
      
     await repository.DeleteItemAsync(id);

      return NoContent();
    }

}

}