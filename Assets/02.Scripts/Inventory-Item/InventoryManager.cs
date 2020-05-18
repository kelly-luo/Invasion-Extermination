using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public Inventory PlayerInventory { get; set; }

    private int maxSlot;
    private int maxWeapons;
    private int currentSlotUsed;

    private GameObject[] slots;
    public GameObject inventoryPanel;

    public GameObject weaponInstances;
    private GameObject[] weaponInstance;
    void Start()
    {
        Intialize();
    }
    private void Intialize()
    {
        currentSlotUsed = 0;
        PlayerInventory = new Inventory();
        maxSlot = inventoryPanel.transform.childCount;
        slots = new GameObject[maxSlot];

        for (int i = 0; i < maxSlot; i++)
        {
            slots[i] = inventoryPanel.transform.GetChild(i).gameObject;
        }

        maxWeapons = weaponInstances.transform.childCount;
        weaponInstance = new GameObject[maxWeapons];
        for (int i = 0; i < maxWeapons; i++)
        {
            weaponInstance[i] = weaponInstances.transform.GetChild(i).gameObject;
        }

        ItemCreater();
    }

    private void ItemCreater()
    {
        PlayerInventory.Add(new Item(0, 0, 100, 100));
        PlayerInventory.Add(new Item(1, 1, 100, 100));
        PlayerInventory.Add(new Item(2, 2, 100, 100));
        PlayerInventory.Add(new Item(1, 3, 100, 100));
        PlayerInventory.Add(new Item(3, 4, 100, 100));
        PlayerInventory.Add(new Item(4, 5, 100, 100));
    }

    public void UpdateInventoryGUI()
    {
        currentSlotUsed = PlayerInventory.GetSize();
        Image slotImage;
        GameObject currentSlot;
        foreach(KeyValuePair<int,Item> Weapon in PlayerInventory.inventory)
        {
            Debug.Log(Weapon.Key + " , " + Weapon.Value.Id);
            currentSlot = slots[Weapon.Key].transform.GetChild(0).gameObject;
            slotImage = currentSlot.GetComponent<Image>();
            slotImage.sprite = weaponInstance[Weapon.Value.Id].GetComponent<SpriteRenderer>().sprite;
            if (currentSlot.activeSelf == false) currentSlot.SetActive(true);
        }
    }

    void Update()
    {
        if (currentSlotUsed != PlayerInventory.GetSize()) UpdateInventoryGUI();
    }
}
