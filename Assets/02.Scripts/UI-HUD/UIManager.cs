﻿using System.Collections;
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

    public PlayerInformation playerInformation;

    public GameObject GameMenuPanel;

    //Temp values, change to 0 after testing
    public int displaymoney;
    public int displayscore;
    public int displayammo;
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
            SetAmmo(0);
            SetMoney(playerInformation.Money);
            SetScore(playerInformation.Score);
        }

        invManager.Intialize(playerInformation.PlayerInventory);
        invManager.UpdateWeaponSlots();

        if (GameMenuPanel != null) VisibleOnScreen(GameMenuPanel);
        if (invManager.inventoryPanel != null) VisibleOnScreen(invManager.inventoryPanel);
    }

    private void Update()
    {
        if(displayhealth != Convert.ToInt32(playerInformation.Health)) SetHealth(playerInformation.Health);
        if (displaymoney != playerInformation.Money) SetMoney(playerInformation.Money);
        // if (displayammo != playerInformation.Ammo) SetScore(playerInformation.Ammo);
        if (displayscore != playerInformation.Score) SetScore(playerInformation.Score);

        CheckInput();
    }

    public void CheckInput()
    {

        if (UnityService.GetKeyUp(KeyCode.I))
        {
            if (invManager.inventoryPanel != null) VisibleOnScreen(invManager.inventoryPanel);
        }

        if (UnityService.GetKeyUp(KeyCode.Escape))
        {
            if (invManager != null)
                if(invManager.inventoryPanel.transform.localScale.x == 1) VisibleOnScreen(invManager.inventoryPanel);
            if (GameMenuPanel != null) VisibleOnScreen(GameMenuPanel);
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
    public void SetAmmo(int ammo)
    {
        displayammo = ammo;
        if (ammo <= 0) displayammo = 0;
        else displayammo = ammo;

        if (ammoView != null) ammoView.text = FormatValue(displayammo);
       
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
    #endregion
}
