using WebAPIWithCrud.Models;

namespace WebAPIWithCrud.Services;

/// <summary>
/// Business logic for items: in-memory storage and CRUD operations.
/// </summary>
public class ItemsService
{
    private readonly List<Item> _items = new();
    private int _nextId = 1;

    public IEnumerable<Item> GetAll() => _items;

    public Item? GetById(int id)
    {
        foreach (var item in _items)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }

    public Item? GetByName(string name)
    {
        foreach (var item in _items)
        {
            if (item.Name == name)
                return item;
        }
        return null;
    }

    public Item Create(Item item)
    {
        item.Id = _nextId;
        _nextId++;
        _items.Add(item);
        return item;
    }

    public bool Update(int id, Item item)
    {
        Item? existingItem = null;

        foreach (Item candidate in _items)
        {
            if (candidate.Id == id)
            {
                existingItem = candidate;
                break;
            }
        }

        if (existingItem == null)
            return false;

        existingItem.Name = item.Name;
        existingItem.Description = item.Description;
        return true;
    }

    public bool Delete(int id)
    {
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            if (_items[i].Id == id)
            {
                _items.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

}
