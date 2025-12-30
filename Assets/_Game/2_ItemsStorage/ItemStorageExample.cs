using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorageExample : MonoBehaviour
{
    private void Awake()
    {
        ItemStorage itemsStorage = new ItemStorage();

        List<Item> items = itemsStorage.GetItemsBy((item) => item.Type == ItemType.Fire);

        Debug.Log($"Фильтр по типу");

        foreach (Item item in items)
            Debug.Log($"{item.GetInfo()}");
    }

    private bool SwordFilter(Item item) => item.Title == "Sword";
    private bool WeightFilter(Item item) => item.Weight > 1;
}