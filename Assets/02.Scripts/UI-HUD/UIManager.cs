using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    readonly private IUnityServiceManager UnityServiceManager = new UnityServiceManager();

    public Slider healthView;
    public Image healthFill;

    public TMP_Text scoreView;
    public TMP_Text moneyView;
    public TMP_Text ammoView;

    public GameObject inventoryPanel;

    //Temp values, change to 0 after testing
    public int money;
    public int score;
    public int ammo;
    public int health;

    public byte test;
    void Start()
    {
        healthView.value = health;
        scoreView.text = FormatValue(score);
        moneyView.text = FormatValue(money);
        ammoView.text = FormatValue(ammo);

    }

    private void Update()
    {
        SetHealth(health);
        SetAmmo(ammo);
        SetMoney(money);
        SetScore(score);

        if (UnityServiceManager.GetKeyDown(KeyCode.I))
        {
           OpenInventory();
        }

    }
    public void SetHealth(int health)
    {
        if(health <= 0) healthView.value = 0;
        else if(health >=100) healthView.value = 100;
        else healthView.value = health;

        if(health <= 50 && health > 30) healthFill.color = Color.yellow;
        else if (health <=30 ) healthFill.color = Color.red;
        else healthFill.color = Color.white;
    }

    public void SetScore(int score)
    {
        if (score <= 0) scoreView.text = FormatValue(0);
        else scoreView.text = FormatValue(score);
    }
    public void SetMoney(int money)
    {
        if (money <= 0) moneyView.text = FormatValue(0);
        else moneyView.text = FormatValue(money);
    }
    public void SetAmmo(int ammo)
    {
        if (ammo <= 0) ammoView.text = FormatValue(0);
        else ammoView.text = FormatValue(ammo);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    private string FormatValue(int value)
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
