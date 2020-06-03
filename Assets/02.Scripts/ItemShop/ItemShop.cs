using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public List<ShopItem> weaponsList;
    public int numWeapons = 9;

    public void PopulateShop()
    {
        PopulateWeaponsList();
    }

    private void PopulateWeaponsList()
    {
        for (int i = 0; i < numWeapons - weaponsList.Count; i++)
        {
            weaponsList.Add(new ShopItem());
        }
    }

    public class ShopItem
    {
        private IUnityServiceManager UnityServiceManager;
        public GameObject[] guns;
        public int costLowerRange = 900;
        public int costUpperRange = 1100;
        int money;
        ImItem item;

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
