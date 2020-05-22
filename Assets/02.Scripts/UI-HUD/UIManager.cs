using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Slider healthView;
    public Image healthFill;

    public TMP_Text scoreView;
    public TMP_Text moneyView;
    public TMP_Text ammoView;

    public InventoryManager invManager;

    [SerializeField] private PlayerInformation playerInformation;

    //Temp values, change to 0 after testing
    public int displaymoney;
    public int displayscore;
    public int displayammo;
    public int displayhealth;

    void Start()
    {

        Intialize();
    }

    public void Intialize()
    {
        if(playerInformation != null)
        {
            SetHealth(playerInformation.Health);
            SetAmmo(0);
            SetMoney(playerInformation.Money);
            SetScore(playerInformation.Score);
        }

        invManager.Intialize(playerInformation.PlayerInventory);
        //invManager.UpdateWeaponSlots();
    }

    private void Update()
    {
        if(displayhealth != Convert.ToInt32(playerInformation.Health)) SetHealth(playerInformation.Health);
        if (displaymoney != playerInformation.Money) SetMoney(playerInformation.Money);
        // if (displayammo != playerInformation.Ammo) SetScore(playerInformation.Ammo);
        if (displayscore != playerInformation.Score) SetScore(playerInformation.Score);

        if (Input.GetKeyDown(KeyCode.I))
        {
            invManager.InventoryVisible();
        }
    }
    public void SetHealth(float fhealth)
    {
        int health = Convert.ToInt32(fhealth);
        if(health <= 0) displayhealth = 0;
        else if(health >=100) displayhealth = 100;
        else displayhealth = health;

        if(healthView != null && healthFill != null) {
            if (displayhealth <= 50 && displayhealth > 30) healthFill.color = Color.yellow;
            else if (displayhealth <= 30) healthFill.color = Color.red;
            else healthFill.color = Color.white;
            healthView.value = displayhealth;
        }
        
    }

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
    public void SetAmmo(int ammo)
    {
        displayammo = ammo;
        if (ammo <= 0) displayammo = 0;
        else displayammo = ammo;

        if (ammoView != null) ammoView.text = FormatValue(displayammo);
       
    }


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

}
