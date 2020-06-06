using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ItemShopTest
    {
        ItemShop itemShop;
        ShopItem shopItem;

        [SetUp]
        public void setup()
        {
            itemShop = new ItemShop();
            itemShop.information = new GameObject().AddComponent<PlayerInformation>();
            itemShop.information.Money = 10000;
            shopItem = new ShopItem();

            shopItem.guns = new GameObject[4];
            for(int i = 0; i<4; i++)
            {
                shopItem.guns[i] = new GameObject();
                shopItem.guns[i].AddComponent<WeaponAK74>();
            }

            shopItem.Instantiate();
        }

        [Test]
        public void check_PopulateList_method_fills_all_nulls()
        {
            //ARRANGE 
            itemShop = new ItemShop();
            itemShop.weaponsArray = new ShopItem[itemShop.numWeapons];

            //ACT
            itemShop.PopulateWeaponsList();

            //ASSERT
            foreach (ShopItem testItem in itemShop.weaponsArray)
            {
                Assert.IsNotNull(testItem);
            }
        }

        [Test]
        public void check_PopulateList_method_fills_null()
        {
            //ARRANGE 
            itemShop = new ItemShop();
            itemShop.weaponsArray[5] = null;

            //ACT
            itemShop.PopulateWeaponsList();

            //ASSERT
            Assert.IsNotNull(itemShop.weaponsArray[5]);
        }

        [Test]
        public void buying_item_changes_that_weapon_in_weaponsList()
        {
            //ARRANGE
            itemShop = new ItemShop();
            shopItem = itemShop.weaponsArray[0];

            //ACT
            itemShop.BuyItem(0);

            //ASSERT
            Assert.AreNotEqual(shopItem, itemShop.weaponsArray[0]);
        }


    }
}
