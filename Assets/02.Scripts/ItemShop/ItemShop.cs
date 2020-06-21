//
// ItemShop
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// This class contains an array of ShopItems, an Item class with a cost attached to it. 
// The class initializes on start, populating the array of ShopItems.
// 
// AUT University - 2020 - Howard Mao
//
//
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
    //The prefabs for gunPrefabs underneath come from clicking and dragging prefabs into the script on unity
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

    //
    // BuyItem()
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // The BuyItem() method puts the item from the given index into the player's inventory and
    // takes away the right amount of money from them.
    //
    // index        the index of the item to be bought
    //
    // returns      true if an item is bought, false if the item isn't bought (due to not enough money)
    public Boolean BuyItem(int index)
    {
        ShopItem buying = weaponsArray[index];
        if (PlayerInfo.Money >= buying.cost )
        {
            if (PlayerInfo.PlayerInventory.Add(buying.item))
            {
                PlayerInfo.Money -= buying.cost;
                weaponsArray[index] = null;
                PopulateWeaponsList();
                return true;
            }
        }
        return false;
    }

    public void BuyAmmo()
    {
        if (PlayerInfo.Money >= ammoCost)
        {
            PlayerInfo.Money -= ammoCost;
            PlayerInfo.ammo += ammoBatch;
        }
    }
}
