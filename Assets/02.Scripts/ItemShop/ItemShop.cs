using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public ShopItem[] weaponsArray;
    public int numWeapons = 9;
    public PlayerInformation PlayerInfo { get; set; }
    public int ammoCost = 10; 
    public int ammoBatch = 10;
    public GameObject[] gunPrefabs;


    void Start()
    {
        PlayerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInformation>();
        weaponsArray = new ShopItem[numWeapons];
        this.PopulateWeaponsList();
    }

    public void PopulateWeaponsList()
    {
        for (int i = 0; i < numWeapons; i++)
            weaponsArray[i] = weaponsArray[i] ?? new ShopItem().InstantiateShopItem(gunPrefabs);
    }

    public Boolean BuyItem(int index)
    {
        ShopItem buying = weaponsArray[index];
        if (PlayerInfo.Money >= buying.cost)
        {
            PlayerInfo.Money -= buying.cost;
            PlayerInfo.PlayerInventory.Add(buying.item);

            weaponsArray[index] = null;
            PopulateWeaponsList();
            return true;
        }
        return false;
    }

    public void BuyAmmo()
    {
        if (PlayerInfo.Money >= ammoCost)
        {
            PlayerInfo.Money -= ammoCost;
            PlayerInfo.Ammo += ammoBatch;
        }
    }
}
