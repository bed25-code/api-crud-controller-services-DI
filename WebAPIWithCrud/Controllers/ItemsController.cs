using Microsoft.AspNetCore.Mvc;
using WebAPIWithCrud.Models;
using WebAPIWithCrud.Services;

namespace WebAPIWithCrud.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly ItemsService _itemsService;

    public ItemsController(ItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    //Gammal get, utan Query parameter
    //[HttpGet]
    //public ActionResult<IEnumerable<Items>> GetAll()
    //{
    //    return Ok(_itemsService.GetAll());
    //}

    [HttpGet]
    public ActionResult<IEnumerable<Item>> Get([FromQuery] string? name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var item = _itemsService.GetByName(name);
            if (item == null) return NotFound();
            return Ok(new[] { item }); // eller Ok(item) om du vill returnera en Item
        }

        return Ok(_itemsService.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Item> Get(int id)
    {
        var item = _itemsService.GetById(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    [HttpPost]
    public ActionResult<Item> Create(Item item)
    {
        var created = _itemsService.Create(item);
        return Created($"/api/items/{created.Id}", created);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, Item item)
    {
        if (!_itemsService.Update(id, item))
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        if (!_itemsService.Delete(id))
            return NotFound();
        return NoContent();
    }

}
