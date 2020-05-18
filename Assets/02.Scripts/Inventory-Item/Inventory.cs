using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Item Primary { get; set; }
    public Item Secondary { get; set; }
    //key = instance id, value = Item
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();

    public void Add(Item item)
    {
        if (inventory.Count < 24) { 
            inventory.Add(item.InstanceId, item);
            if (Primary == null)
            {
                SetPrimary(item);
            }
            else if (Secondary == null)
            {
                SetSecondary(item);
            }
        }
    }

    public bool ContainsKey(int itemID)
    {
        return inventory.ContainsKey(itemID);
    }

    public bool Remove(Item item)
    {
            if (Primary == item) SetPrimary(null);
            else if (Secondary == item) SetSecondary(null);

            return inventory.Remove(item.InstanceId);
    }

    public void SetPrimary(Item item)
    {
        if (Secondary == item) Secondary = Primary;
        Primary = item;
    }

    public void SetSecondary(Item item)
    {
        if (Primary == item) Primary = Secondary;
        Secondary = item;
    }
}
