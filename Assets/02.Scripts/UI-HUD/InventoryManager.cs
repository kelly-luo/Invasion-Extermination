using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Inventory playerInventory;

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

    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    bool visible = true;
    public void Intialize(Inventory inventory)
    {
        playerInventory = inventory;

        currentSlotUsed = 0;
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
        UpdateWeaponSlots();

    }



    public void UpdateInventoryGUI()
    {
        UpdateWeaponSlots();
        currentSlotUsed = playerInventory.GetSize();
        int slotNo = -1;
        foreach(KeyValuePair<int,ImItem> Weapon in playerInventory.inventory)
        {
            slotNo++;
           // Debug.Log(slotNo + " : " + Weapon.Key + " , " + Weapon.Value.Id);

            bcSlotSelect slotInfo = slots[slotNo].GetComponent<bcSlotSelect>();

            slotInfo.InstanceId = Weapon.Key;
            slotInfo.setSprite(GetImage(Weapon.Value.EntityID));
            slotInfo.stack_text.text = UIManager.FormatValue(Weapon.Value.StackAmount);
        }
      
        for (int i = playerInventory.GetSize()  ; i < slots.Length; i++)
        {
            slots[i].GetComponent<MenuButton>().disableButton();
            slots[i].SetActive(false);
        }
    }

    public void SetPrimary(int key)
    {
        if (playerInventory.ContainsKey(key))
        {
            playerInventory.SetPrimary(key);
            UpdateWeaponSlots();
   
        }
    }

    public void SetSecondary(int key)
    {
        if (playerInventory.ContainsKey(key))
        {
            playerInventory.SetSecondary(key);
            UpdateWeaponSlots();
          
        }
    }

    public void RemoveItem(int key)
    {

        if (playerInventory.ContainsKey(key))
        {
            ImItem RemovedItem = playerInventory.FindItem(key);
            playerInventory.Remove(RemovedItem);

            UpdateWeaponSlots();
        }
    }

    public void UpdateWeaponSlots()
    {
        if(playerInventory.Primary != null)
        {     
            Primary.GetComponent<Image>().sprite = GetImage(playerInventory.Primary.EntityID);
            if (!Primary.activeSelf) Primary.SetActive(true);
        }
        else
            Primary.SetActive(false);
        
        if (playerInventory.Secondary != null)
        {
            Secondary.GetComponent<Image>().sprite = GetImage(playerInventory.Secondary.EntityID);
            if (!Secondary.activeSelf) Secondary.SetActive(true);
        }else
            Secondary.SetActive(false);
    }
    

    private Sprite GetImage(int id)
    {
        int EntityId;
        for (int i = 0; i < maxWeapons; i++)
        {
            EntityId = weaponInstance[i].GetComponent<Enity_ID>().EntityId;
            if(EntityId == id) return weaponInstance[i].GetComponent<SpriteRenderer>().sprite;
        }
        return null;
    }

    public void InventoryVisible()
    {
        if (inventoryPanel != null)
            //inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (visible)
            {
                inventoryPanel.transform.localScale = new Vector3(0, 0, 0);
                visible = false;
            }
            else
            {
                inventoryPanel.transform.localScale = new Vector3(1, 1, 1);
                visible = true;
            }
           
            
    }

    void Update()
    {
        if (playerInventory != null)
        {
            if (currentSlotUsed != playerInventory.GetSize()) UpdateInventoryGUI();
        }

        if (UnityService.GetKeyUp(KeyCode.Alpha1))
        {
            playerInventory.SelectPrimary();
        }

        if (UnityService.GetKeyUp(KeyCode.Alpha2))
        {
            playerInventory.SelectSecondary();
        }

    }
}
