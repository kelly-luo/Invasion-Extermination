using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    
    public Item Primary { get; set; }
    public Item Secondary { get; set; }

    public Item selected;
    //key = instance id, value = Item
    public Dictionary<int, Item> inventory = new Dictionary<int, Item>();

    
    public void Add(Item item)
    {
        if (inventory.Count < 24) 
        {
            if (!inventory.ContainsKey(item.InstanceId))
            {
                inventory.Add(item.InstanceId, item);
                if (Primary == null)
                {
                    SetPrimary(item);
                    selected = Primary;
                }
                else if (Secondary == null)
                {
                    SetSecondary(item);
                }
            }
            else
            {
                StackUpItem(item);
            }
        }
    }

    private void StackUpItem(Item item)
    {
        inventory.TryGetValue(item.InstanceId, out Item value);
        value.Amount += item.Amount;
        inventory[item.InstanceId] = value;
    }

    public bool ContainsKey(int itemID)
    {
        return inventory.ContainsKey(itemID);
    }

    public void SelectPrimary()
    {
        if(Primary != null)
            selected = Primary; 
    }

    public void SelectSecondary()
    {
        if(Secondary != null)
            selected = Secondary;
    }

    public bool Remove(Item item)
    {
        if (Primary == item) 
            SetPrimary(null);
        else if (Secondary == item) 
            SetSecondary(null);
        return inventory.Remove(item.InstanceId);
    }

    public void SetPrimary(Item item)
    {
        if (Secondary == item) 
            Secondary = Primary;
        Primary = item;
        SelectPrimary();
    }

    public void SetSecondary(Item item)
    {
        if (Primary == item) 
            Primary = Secondary;
        Secondary = item;
        SelectSecondary();
    }

    public void SetPrimary(int key)
    {
        SetPrimary(FindItem(key));
    }
    public void SetSecondary(int key)
    {
        SetSecondary(FindItem(key));
    }
    public void Remove(int key)
    {
        Remove(FindItem(key));
    }

    public Item FindItem(int key)
    {
        if (ContainsKey(key) == false) return null;

        return inventory[key];
    }

    public int GetSize()
    {
        return inventory.Count;
    }
}
