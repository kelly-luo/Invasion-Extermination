using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();

    public void Add(Item item)
    {
        throw new NotImplementedException();
        //inventory.Add(item.Id, item);
    }

    public bool ContainsKey(int itemID)
    {
        throw new NotImplementedException();
        //return inventory.ContainsKey(itemID);
    }
}
