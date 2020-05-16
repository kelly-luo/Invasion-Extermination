using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Inventory
{
    public class InventoryTest
    {
        [Test]
        public void inventory_allows_pickup_of_one_item()
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
        public void inventory_allows_multiple_item_pickup()
        {
            //ARRANGE
            Inventory inventory = new Inventory();
            Item itemOne = new Item(1, 1, 2, 1);
            Item itemTwo = new Item(2, 2, 5, 1);

            //ACT
            inventory.Add(itemOne);
            inventory.Add(itemTwo);

            //ASSERT
            Assert.AreEqual(true, inventory.ContainsKey(itemOne.InstanceId));
            Assert.AreEqual(true, inventory.ContainsKey(itemTwo.InstanceId));

        }

        [Test]
        public void inventory_allows_removal_of_one_item()
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
        public void allow_equip_to_primary()
        {
            //ARRANGE
            Inventory inventory = new Inventory();
            Item item = new Item(1, 1, 2, 1);
            inventory.Add(item);

            //ACT
            inventory.setPrimary(item);

            //ASSERT
            Assert.AreEqual(item,inventory.primary);
        }
    }
}
