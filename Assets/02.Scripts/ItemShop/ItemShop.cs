using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class ItemShop 
{
    public ShopItem[] weaponsArray;
    public int numWeapons = 9;
    public PlayerInformation information;
    public int ammoCost = 10; 
    public int ammoBatch = 10;

    public ItemShop()
    {
        weaponsArray = new ShopItem[numWeapons];
        PopulateWeaponsList();
    }

    public void PopulateWeaponsList()
    {
        for (int i = 0; i < numWeapons; i++)
            weaponsArray[i] = weaponsArray[i] ?? new ShopItem().Instantiate();
    }

    public void BuyItem(int index)
    {
        ShopItem buying = weaponsArray[index];
        if(information.Money >= buying.money)
        {
            information.Money -= buying. money;
            information.PlayerInventory.Add(buying.item);

            weaponsArray[index] = null;
            PopulateWeaponsList();
        }
    }

    public void BuyAmmo()
    {
        if (information.Money >= ammoCost)
        {
            information.Money -= ammoCost;
            information.Ammo += ammoBatch;
        }
    }
}
