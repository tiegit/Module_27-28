using System;
using System.Collections.Generic;

public class ItemStorage
{
    private List<Item> _items;

    public ItemStorage()
    {
        _items = new List<Item>();

        _items.Add(new Item("Sword", ItemType.Fire, 1));
        _items.Add(new Item("Sword", ItemType.Frozen, 2));
        _items.Add(new Item("Sword", ItemType.Wind, 2));

        _items.Add(new Item("Shield", ItemType.Fire, 1));
        _items.Add(new Item("Shield", ItemType.Frozen, 3));
        _items.Add(new Item("Shield", ItemType.Wind, 1));

        _items.Add(new Item("Axe", ItemType.Fire, 1));
        _items.Add(new Item("Axe", ItemType.Frozen, 1));
        _items.Add(new Item("Axe", ItemType.Wind, 1));
    }

    public List<Item> GetItemsBy(Func<Item, bool> itemFilter)
    {
        List<Item> selectedItems = new List<Item>();

        foreach (Item item in _items)
        {
            if(itemFilter.Invoke(item))
                selectedItems.Add(item);
        }

        return selectedItems;
    }
    //public List<Item> GetItemsBy(ItemFilter itemFilter)
    //{
    //    List<Item> selectedItems = new List<Item>();

    //    foreach (Item item in _items)
    //    {
    //        if(itemFilter.Invoke(item))
    //            selectedItems.Add(item);
    //    }

    //    return selectedItems;
    //}
}
