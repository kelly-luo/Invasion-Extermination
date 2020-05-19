using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class InventoryTest
{
    [Test]
    public void allows_pickup_of_one_item()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item item = new Item(1, 1, 2, 1);

        //ACT
        inventory.Add(item);

        //ASSERT
        Assert.AreEqual(true, inventory.ContainsKey(item.InstanceId));

    }

    [Test]
    public void allows_multiple_item_pickup()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(2, 2, 5, 1);
        Item itemThree = new Item(2, 3, 5, 1);

        //ACT
        inventory.Add(itemOne);
        inventory.Add(itemTwo);
        inventory.Add(itemThree);

        //ASSERT
        Assert.AreEqual(true, inventory.ContainsKey(itemOne.InstanceId));
        Assert.AreEqual(true, inventory.ContainsKey(itemTwo.InstanceId));
        Assert.AreEqual(true, inventory.ContainsKey(itemThree.InstanceId));

    }

    [Test]
    public void allows_removal_of_one_item()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item item = new Item(1, 1, 2, 1);
        inventory.Add(item);

        //ACT
        inventory.Remove(item);

        //ASSERT
        Assert.AreEqual(false, inventory.ContainsKey(item.InstanceId));
    }

    [Test]
    public void allows_removal_of_multiple_items()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(1, 2, 2, 1);
        Item itemThree = new Item(1, 3, 2, 1);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);
        inventory.Add(itemThree);

        //ACT
        inventory.Remove(itemOne);
        inventory.Remove(itemTwo);
        inventory.Remove(itemThree);

        //ASSERT
        Assert.AreEqual(false, inventory.ContainsKey(itemOne.InstanceId));
        Assert.AreEqual(false, inventory.ContainsKey(itemTwo.InstanceId));
        Assert.AreEqual(false, inventory.ContainsKey(itemThree.InstanceId));
    }

    [Test]
    public void automatically_places_item_in_primary()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item item = new Item(1, 1, 2, 1);

        //ACT
        inventory.Add(item);

        //ASSERT
        Assert.AreEqual(item, inventory.Primary);
    }

    [Test]
    public void automatically_places_first_item_in_primary()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(1, 2, 2, 1);

        //ACT
        inventory.Add(itemOne);
        inventory.Add(itemTwo);

        //ASSERT
        Assert.AreEqual(itemOne, inventory.Primary);
    }

    [Test]
    public void allows_removal_of_primary_with_no_other_items_in_inventory()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item item = new Item(1, 1, 2, 1);
        inventory.Add(item);

        //ACT
        inventory.Remove(item);

        //ASSERT
        Assert.Null(inventory.Primary);
    }

    [Test]
    public void allows_removal_of_primary_with_multiple_items_in_inventory()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(1, 2, 2, 1);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);

        //ACT
        inventory.Remove(itemOne);

        //ASSERT
        Assert.Null(inventory.Primary);
    }

    [Test]
    public void automatically_places_second_item_in_secondary()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(1, 2, 2, 1);
        inventory.Add(itemOne);

        //ACT
        inventory.Add(itemTwo);

        //ASSERT
        Assert.AreEqual(itemTwo, inventory.Secondary);
    }

    [Test]
    public void allows_removal_of_secondary()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(1, 2, 2, 1);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);

        //ACT
        inventory.Remove(itemTwo);

        //ASSERT
        Assert.Null(inventory.Secondary);
    }

    public void allows_removal_of_secondary_with_multiple_other_items_in_inventory()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 1);
        Item itemTwo = new Item(1, 2, 2, 1);
        Item itemThree = new Item(1, 3, 2, 1);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);
        inventory.Add(itemThree);

        //ACT
        inventory.Remove(itemTwo);

        //ASSERT
        Assert.Null(inventory.Secondary);
    }

    [Test]
    public void can_set_primary_to_be_a_different_item()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 3);
        Item itemTwo = new Item(1, 2, 9, 3);
        Item itemThree = new Item(1, 3, 9, 3);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);
        inventory.Add(itemThree);

        //ACT
        inventory.SetPrimary(itemThree);

        //ASSERT
        Assert.AreEqual(itemThree, inventory.Primary);
    }

    [Test]
    public void can_set_secondary_to_be_a_different_item()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 3);
        Item itemTwo = new Item(1, 2, 9, 3);
        Item itemThree = new Item(1, 3, 9, 3);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);
        inventory.Add(itemThree);

        //ACT
        inventory.SetSecondary(itemThree);

        //ASSERT
        Assert.AreEqual(itemThree, inventory.Secondary);
    }

    [Test]
    public void can_swap_primary_and_secondary_by_changing_primary()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 3);
        Item itemTwo = new Item(1, 2, 9, 3);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);

        //ACT
        inventory.SetPrimary(itemTwo);

        //ASSERT
        Assert.AreEqual(itemTwo, inventory.Primary);
        Assert.AreEqual(itemOne, inventory.Secondary);
    }

    [Test]
    public void can_swap_primary_and_secondary_by_changing_secondary()
    {
        //ARRANGE
        Inventory inventory = new Inventory();
        Item itemOne = new Item(1, 1, 2, 3);
        Item itemTwo = new Item(1, 2, 9, 3);
        inventory.Add(itemOne);
        inventory.Add(itemTwo);

        //ACT
        inventory.SetSecondary(itemOne);

        //ASSERT
        Assert.AreEqual(itemOne, inventory.Secondary);
        Assert.AreEqual(itemTwo, inventory.Primary);
    }

}

