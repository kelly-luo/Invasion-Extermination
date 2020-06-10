using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;
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
            itemShop = new GameObject().AddComponent<ItemShop>();
            itemShop.PlayerInfo = new GameObject().AddComponent<PlayerInformation>();
            itemShop.PlayerInfo.Money = 10000;
            shopItem = new ShopItem();

            itemShop.PlayerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInformation>();
            itemShop.weaponsArray = new ShopItem[itemShop.numWeapons];
            itemShop.gunPrefabs = new GameObject[6];

            GameObject ak74 = new GameObject();
            ak74.AddComponent<WeaponAK74>();
            itemShop.gunPrefabs[0] = ak74;

            GameObject benelli = new GameObject();
            benelli.AddComponent<WeaponBennelli_M4>();
            itemShop.gunPrefabs[1] = benelli;

            GameObject m107 = new GameObject();
            m107.AddComponent<WeaponM107>();
            itemShop.gunPrefabs[2] = m107;

            GameObject m1911 = new GameObject();
            m1911.AddComponent<WeaponM1911>();
            itemShop.gunPrefabs[3] = m1911;

            GameObject m249 = new GameObject();
            m249.AddComponent<WeaponM249>();
            itemShop.gunPrefabs[4] = m249;

            GameObject m4_8 = new GameObject();
            m4_8.AddComponent<WeaponM4_8>();
            itemShop.gunPrefabs[5] = m4_8;

            itemShop.PopulateWeaponsList();
        }
        [Test]
        public void check_PopulateList_method_fills_all_nulls()
        {
            //ARRANGE 
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
            shopItem = itemShop.weaponsArray[0];
            itemShop.PlayerInfo.Money = 10000;

            //ACT
            itemShop.BuyItem(0);

            //ASSERT
            Assert.AreNotSame(shopItem, itemShop.weaponsArray[0]);
        }
        [Test]
        public void cant_buy_item_when_you_dont_have_money()
        {
            //ARRANGE
            itemShop.PlayerInfo.Money = 10;
            ShopItem item = itemShop.weaponsArray[0];

            //ACT
            itemShop.BuyItem(0);

            //ASSERT
            Assert.AreEqual(item, itemShop.weaponsArray[0]);
        }
        [Test]
        public void buying_item_removes_correct_amount_from_player()
        {
            //ARRANGE
            itemShop.PlayerInfo.Money = 1000;
            itemShop.weaponsArray[2].cost = 900;
            ShopItem item = itemShop.weaponsArray[2];
            int cost = item.cost;

            //ACT
            itemShop.BuyItem(2);

            //ASSERT
            Assert.AreEqual(100,itemShop.PlayerInfo.Money);
        }
        [Test]
        public void buying_item_gives_player_item()
        {
            //ARRANGE
            itemShop.PlayerInfo.Money = 10000;
            ShopItem item = itemShop.weaponsArray[4];

            //ACT
            itemShop.BuyItem(4);

            //ASSERT
            Assert.AreSame(item.item, itemShop.PlayerInfo.PlayerInventory.GetItem(item.item));

        }
        [Test]
        public void buy_ammo()
        {
            //ARRANGE
            itemShop.PlayerInfo.Money = 10000;

            //ACT
            itemShop.BuyAmmo();

            //ASSERT
            Assert.AreEqual(110, itemShop.PlayerInfo.ammo);
        }
        [Test]
        public void buying_ammo_removes_money()
        {
            //ARRANGE
            itemShop.PlayerInfo.Money = 10000;

            //ACT
            itemShop.BuyAmmo();

            //ASSERT
            Assert.AreEqual(9990, itemShop.PlayerInfo.Money);
        }
        [TearDown]
        public void tearDown()
        {
            itemShop = null;
            shopItem = null;
        }

    }
}
