using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class WeaponStatsTest
    {
        ShopItem shopItem;
        ItemShop itemShop;

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
        public void can_create_weapon()
        {
            //ACT
            shopItem.InstantiateShopItem(itemShop.gunPrefabs);

            //ASSERT
            Assert.IsNotNull(shopItem.item);
        }
        [Test]
        public void weapon_has_cost_between_900_1100()
        {
            //ARRANGE
            shopItem.InstantiateShopItem(itemShop.gunPrefabs);

            //ASSERT
            Assert.GreaterOrEqual(shopItem.cost, 900);
            Assert.LessOrEqual(shopItem.cost, 1100);
        }

    }
}
