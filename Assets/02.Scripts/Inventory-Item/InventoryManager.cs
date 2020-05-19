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

    public GameObject inventoryPanel;
    private GameObject[] slots;

    public GameObject weaponInstances;
    private GameObject[] weaponInstance;

    public GameObject Primary;
    public GameObject Secondary;


    public int temp;
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
        UpdateWeaponSlots();
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

    public void SetPrimary(int key)
    {
        if (PlayerInventory.ContainsKey(key)) 
        {
            PlayerInventory.SetPrimary(key);
            UpdateWeaponSlots();
        }
    }

    public void SetSecondary(int key)
    {
        if (PlayerInventory.ContainsKey(key))
        {
            PlayerInventory.SetSecondary(key);
            UpdateWeaponSlots();
        }
    }

    public void removeItem(int key)
    {
        if (PlayerInventory.ContainsKey(key))
        {
            Debug.Log("Removed="+key);
            Item RemovedItem = PlayerInventory.FindItem(key);
            PlayerInventory.Remove(RemovedItem);
            
            slots[key].transform.GetChild(0).gameObject.SetActive(false);
            UpdateWeaponSlots();

        }
    }

    public void UpdateWeaponSlots()
    {
        if(PlayerInventory.Primary != null)
        { 
            Primary.GetComponent<Image>().sprite = weaponInstance[PlayerInventory.Primary.Id].GetComponent<SpriteRenderer>().sprite;
            if (!Primary.activeSelf) Primary.SetActive(true);
        }
        else
            Primary.SetActive(false);
        
        if (PlayerInventory.Secondary != null)
        {
            Secondary.GetComponent<Image>().sprite = weaponInstance[PlayerInventory.Secondary.Id].GetComponent<SpriteRenderer>().sprite;
            if (!Secondary.activeSelf) Secondary.SetActive(true);
        }else
            Secondary.SetActive(false);
    }

    void Update()
    {
        if (currentSlotUsed != PlayerInventory.GetSize()) UpdateInventoryGUI();
    }
}
