//
// Inventory class
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// Stores the player's inventory (ImItem) in a Dictionary. Keeps track of which weapons are the player's primary
// and secondary.
// 
// AUT university - 2020 - Howard Mao and Yuki Liyanage
//

using System.Collections.Generic;

public class Inventory
{
    
    public ImItem Primary { get; set; }
    public ImItem Secondary { get; set; }

    public ImItem selected;
    //key = instance id, value = Item
    public Dictionary<int, ImItem> inventory = new Dictionary<int, ImItem>();

    
    public bool Add(ImItem item)
    {
        bool addedItem = false;
        if (inventory.Count < 24) 
        {
            addedItem = true;
            if (!inventory.ContainsKey(item.InstanceID))
            {
                inventory.Add(item.InstanceID, item);
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
        return addedItem;
    }

    //
    // StackUpItem
    // ~~~~~~~~~~~~~~~~~~~~~
    // If there's more than one of an item with the same instanceID, the items "stack" together to reduce space
    // in the player's inventory.
    //
    // item     checks this item for a duplicate in the inventory
    private void StackUpItem(ImItem item)
    {
        inventory.TryGetValue(item.InstanceID, out ImItem value);
        value.StackAmount += item.StackAmount;
        inventory[item.InstanceID] = value;
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

    public ImWeapon Equppied()
    {
        return (ImWeapon)selected;
    }

    public bool Remove(ImItem item)
    {
        if (Primary == item) 
            SetPrimary(null);
        else if (Secondary == item) 
            SetSecondary(null);
        return inventory.Remove(item.InstanceID);
    }

    public void SetPrimary(ImItem item)
    {
        if (Secondary == item) 
            Secondary = Primary;
        Primary = item;
        SelectPrimary();
    }

    public void SetSecondary(ImItem item)
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

    public ImItem FindItem(int key)
    {
        if (ContainsKey(key) == false) return null;

        return inventory[key];
    }

    public int GetSize()
    {
        return inventory.Count;
    }

    public ImItem GetItem(ImItem item)
    {
        if (this.inventory.ContainsValue(item)) return item;
        return null;
    }

    public ImItem GetItem(int key)
    {
        if (this.inventory.ContainsKey(key))
        {
            ImItem value;
            this.inventory.TryGetValue(key, out value);
            return value;
        }
        return null;
    }

}
