using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject healthObject;
    public Slider healthView;
    public Image healthFill;

    public TMP_Text scoreView;
    public TMP_Text moneyView;
    public TMP_Text gunammoView;
    public TMP_Text amountammoView;

    public InventoryManager invManager;

    public ShopManager shopManager;

    public PlayerInformation playerInformation;

    public GameObject GameMenuPanel;

    //Temp values, change to 0 after testing
    public int displaymoney;
    public int displayscore;
    public int displaygunammo;
    public int displayamountammo;
    public int displayhealth;


    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;

    void Start()
    {
        Intialize();
    }

    public void Intialize()
    {

        if(playerInformation != null)
        {
            SetHealth(playerInformation.Health);
            SetAmountAmmo(playerInformation.Ammo);
            SetMoney(playerInformation.Money);
            SetScore(playerInformation.Score);
        }

        invManager.Intialize(playerInformation.PlayerInventory);
        invManager.UpdateWeaponSlots();

        if (shopManager != null) VisibleOnScreen(shopManager.gameObject);
        if (GameMenuPanel != null) VisibleOnScreen(GameMenuPanel);
        if (invManager.inventoryPanel != null) VisibleOnScreen(invManager.inventoryPanel);
    }

    void Update()
    {
        if (displayhealth != Convert.ToInt32(playerInformation.Health)) SetHealth(playerInformation.Health);
        if (displaymoney != playerInformation.Money) SetMoney(playerInformation.Money);
        if (displaygunammo != playerInformation.PlayerInventory.Equppied().NumOfBullet) SetGunAmmo(playerInformation.PlayerInventory.Equppied().NumOfBullet);
        if (displayamountammo != playerInformation.Ammo) SetAmountAmmo(playerInformation.Ammo);
        if (displayscore != playerInformation.Score) SetScore(playerInformation.Score);
        CheckInput();
    }

    public void CheckInput()
    {

        if (UnityService.GetKeyUp(KeyCode.I))
        {
            if (invManager.inventoryPanel != null && !IsVisible(shopManager.gameObject)) VisibleOnScreen(invManager.inventoryPanel);
        }

        if (UnityService.GetKeyUp(KeyCode.B))
        {
            if (!IsVisible(shopManager.gameObject))
            {
                makeVisible(invManager.inventoryPanel,shopManager.gameObject);
                makeInvisble(healthObject);
            }
            else
            {
                makeInvisble(invManager.inventoryPanel, shopManager.gameObject);
                makeVisible(healthObject);
            }
        }

        if (UnityService.GetKeyUp(KeyCode.Escape))
        {
            if (!IsVisible(GameMenuPanel))
            {
                makeVisible(GameMenuPanel);
                makeInvisble(invManager.inventoryPanel, shopManager.gameObject, healthObject);
            }
            else
            {
                makeInvisble(GameMenuPanel);
                makeVisible(healthObject);
            }

        }

        if (UnityService.GetKeyUp(KeyCode.Alpha1))
        {
            if (invManager.PlayerInventory != null) invManager.PlayerInventory.SelectPrimary();
        }

        if (UnityService.GetKeyUp(KeyCode.Alpha2))
        {
            if (invManager.PlayerInventory != null) invManager.PlayerInventory.SelectSecondary();
        }
    }

    static public void makeInvisble(params GameObject[] panels)
    {
        foreach(GameObject panel in panels){
            if (IsVisible(panel)) VisibleOnScreen(panel);
        }
    }
    static public void makeVisible(params GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (!IsVisible(panel)) VisibleOnScreen(panel);
        }
    }

    #region Displaying_Information
    public void SetScore(int score)
    {
        displayscore = score;
        if (score <= 0) displayscore = 0;
        else displayscore = score;

        if (scoreView != null) scoreView.text = FormatValue(displayscore);
       
    }
    public void SetMoney(int money)
    {
        displaymoney = money;
        if (money <= 0) displaymoney = 0;
        else displaymoney = money;

        if (moneyView != null) moneyView.text = FormatValue(displaymoney);
       
    }
    public void SetAmountAmmo(int ammo)
    {
        displayamountammo = ammo;
        if (ammo <= 0) displayamountammo = 0;
        else displayamountammo = ammo;

        if (amountammoView != null) amountammoView.text = FormatValue(displayamountammo);
       
    }
    public void SetGunAmmo(int ammo)
    {
        displaygunammo = ammo;
        if (ammo <= 0) displaygunammo = 0;
        else displaygunammo = ammo;

        if (gunammoView != null) gunammoView.text = FormatValue(displaygunammo);

    }

    public void SetHealth(float fhealth)
    {
        int health = Convert.ToInt32(fhealth);
        if (health <= 0) displayhealth = 0;
        else if (health >= 100) displayhealth = 100;
        else displayhealth = health;

        if (healthView != null && healthFill != null)
        {
            if (displayhealth <= 50 && displayhealth > 30) healthFill.color = Color.yellow;
            else if (displayhealth <= 30) healthFill.color = Color.red;
            else healthFill.color = Color.white;
            healthView.value = displayhealth;
        }

    }

    #endregion
    #region Managing_UI
    static public string FormatValue(int value)
    {
        if(value < 10)
        {
            return "0" + value;
        }
        else
        {
            return ""+ value;
        }

    }


    static public void VisibleOnScreen(GameObject panel)
    {
        if (panel.transform.localScale.x == 1)
        {
            panel.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            panel.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    static public bool IsVisible(GameObject panel)
    {
        if (panel.transform.localScale.x == 1)return true;
        else return false;
        
    }
    #endregion
}
