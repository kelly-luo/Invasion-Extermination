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

        [Test]
        public void weapon_has_damage_between_base_and_10perc_extra()
        {
            //ARRANGE
            float damage;
            float initialDamage;
            int entityID;

            //ACT
            shopItem.InstantiateShopItem(itemShop.gunPrefabs);

            if(shopItem.item is ImWeapon gunImWeapon)
            {
                damage = gunImWeapon.Damage;
                entityID = gunImWeapon.EntityID;
                if (gunImWeapon is WeaponAK74 ak)
                    initialDamage = new GameObject().AddComponent<WeaponAK74>().Damage;
                else if (gunImWeapon is WeaponM107 M107)
                    initialDamage = new GameObject().AddComponent<WeaponM107>().Damage;
                else if (gunImWeapon is WeaponM249 M249)
                    initialDamage = new GameObject().AddComponent<WeaponM249>().Damage;
                else if (gunImWeapon is WeaponM4_8 M4_8)
                    initialDamage = new GameObject().AddComponent<WeaponM4_8>().Damage;
                else if (gunImWeapon is WeaponBennelli_M4 M4)
                    initialDamage = new GameObject().AddComponent<WeaponBennelli_M4>().Damage;
                else if (gunImWeapon is WeaponM1911 M19)
                    initialDamage = new GameObject().AddComponent<WeaponM1911>().Damage;
                else initialDamage = 0;

                //ASSERT
                Assert.GreaterOrEqual(damage, initialDamage);
                Assert.LessOrEqual(damage,initialDamage*1.1);
            }
        }
        [Test]
        public void weapon_has_max_bullet_between_base_and_10perc_extra()
        {
            //ARRANGE
            float bullets;
            float initialBullets;
            int entityID;

            //ACT
            shopItem.InstantiateShopItem(itemShop.gunPrefabs);

            if (shopItem.item is ImWeapon gunImWeapon)
            {
                bullets = gunImWeapon.MaxBullet;
                entityID = gunImWeapon.EntityID;
                if (gunImWeapon is WeaponAK74 ak)
                    initialBullets = new GameObject().AddComponent<WeaponAK74>().MaxBullet;
                else if (gunImWeapon is WeaponM107 M107)
                    initialBullets = new GameObject().AddComponent<WeaponM107>().MaxBullet;
                else if (gunImWeapon is WeaponM249 M249)
                    initialBullets = new GameObject().AddComponent<WeaponM249>().MaxBullet;
                else if (gunImWeapon is WeaponM4_8 M4_8)
                    initialBullets = new GameObject().AddComponent<WeaponM4_8>().MaxBullet;
                else if (gunImWeapon is WeaponBennelli_M4 M4)
                    initialBullets = new GameObject().AddComponent<WeaponBennelli_M4>().MaxBullet;
                else if (gunImWeapon is WeaponM1911 M1911)
                    initialBullets = new GameObject().AddComponent<WeaponM1911>().MaxBullet;
                else initialBullets = 123456;

                //ASSERT
                Assert.GreaterOrEqual(bullets, initialBullets);
                Assert.LessOrEqual(bullets, initialBullets*1.1);
            }
        }
        [TearDown]
        public void TearDown()
        {
            shopItem = null;
            itemShop = null;
        }

    }
}
