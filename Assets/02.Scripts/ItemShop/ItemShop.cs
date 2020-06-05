using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public List<ShopItem> weaponsList;
    public int numWeapons = 9;
    public PlayerStateController controller;

    public void PopulateShop()
    {
        PopulateWeaponsList();
    }

    private void PopulateWeaponsList()
    {
        for (int i = 0; i < numWeapons; i++)
            weaponsList[i] = weaponsList[i] ?? new ShopItem();
    }

    public void BuyItem(int index)
    {
        ShopItem buying = weaponsList[index];
        if(controller.playerStats.Money >= buying.money)
        {
            controller.playerStats.Money -= buying.money;
            controller.playerStats.PlayerInventory.Add(buying.item);

            weaponsList.RemoveAt(index);
        }
    }

    public class ShopItem
    {
        private IUnityServiceManager UnityServiceManager;
        public GameObject[] guns;
        public int costLowerRange = 900;
        public int costUpperRange = 1100;
        public int money;
        public ImItem item;

        public ShopItem()
        {
            var gun = guns[UnityServiceManager.UnityRandomRange(0, 4)].GetComponent<ImWeapon>();
            gun.Damage += UnityServiceManager.UnityRandomRange(0, (int)(gun.Damage * 0.1f));
            gun.MaxBullet += UnityServiceManager.UnityRandomRange(0, (int)(gun.MaxBullet * 0.1f));
            this.item = gun;
            this.money = UnityServiceManager.UnityRandomRange(costLowerRange, costUpperRange);
        }
    }
}
