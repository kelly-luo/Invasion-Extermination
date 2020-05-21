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

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventorySlotPanel;
    private GameObject[] slots;

    [SerializeField] private GameObject weaponInstances;
    private GameObject[] weaponInstance;

    [SerializeField] private GameObject Primary;
    [SerializeField] private GameObject Secondary;


    public int temp;
   
    void Start()
    {
        Intialize();
    }
    public void Intialize()
    {
        currentSlotUsed = 0;
        PlayerInventory = new Inventory();
        maxSlot = inventorySlotPanel.transform.childCount;
        slots = new GameObject[maxSlot];

        for (int i = 0; i < maxSlot; i++)
        {
            slots[i] = inventorySlotPanel.transform.GetChild(i).gameObject;
        }

        maxWeapons = weaponInstances.transform.childCount;
        weaponInstance = new GameObject[maxWeapons];
        for (int i = 0; i < maxWeapons; i++)
        {
            weaponInstance[i] = weaponInstances.transform.GetChild(i).gameObject;
        }

        //TempCODE
        ItemCreater();
    }

    //Remove when Items become avaliable
    private void ItemCreater()
    {
        for(int i = 0; i < 10; i++)
        {
            PlayerInventory.Add(new Item(Random.Range(0, 5), Random.Range(0, 20), Random.Range(1, 100), 1));
        }
    }
    //Remove when Items become avaliable

    public void UpdateInventoryGUI()
    {
        UpdateWeaponSlots();
        currentSlotUsed = PlayerInventory.GetSize();
        int slotNo = -1;
        foreach(KeyValuePair<int,Item> Weapon in PlayerInventory.inventory)
        {
            slotNo++;
           // Debug.Log(slotNo + " : " + Weapon.Key + " , " + Weapon.Value.Id);

            bcSlotSelect slotInfo = slots[slotNo].GetComponent<bcSlotSelect>();

            slotInfo.InstanceId = Weapon.Key;
            slotInfo.setSprite(weaponInstance[Weapon.Value.Id].GetComponent<SpriteRenderer>().sprite);
            slotInfo.stack_text.text = UIManager.FormatValue(Weapon.Value.Amount);
        }
      
        for (int i = PlayerInventory.GetSize()  ; i < slots.Length; i++)
        {
             slots[i].GetComponent<MenuButton>().disableButton();
            slots[i].SetActive(false);
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

    public void RemoveItem(int key)
    {

        if (PlayerInventory.ContainsKey(key))
        {

            Item RemovedItem = PlayerInventory.FindItem(key);
            PlayerInventory.Remove(RemovedItem);

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

    public void InventoryVisible()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    void Update()
    {
        if (currentSlotUsed != PlayerInventory.GetSize()) UpdateInventoryGUI();
    }
}
