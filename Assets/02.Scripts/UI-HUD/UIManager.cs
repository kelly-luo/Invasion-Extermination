using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    private IUnityServiceManager UnityServiceManager = new UnityServiceManager();

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
        scoreView.text = formatValue(score);
        moneyView.text = formatValue(money);
        ammoView.text = formatValue(ammo);

    }

    private void Update()
    {
        setHealth(health);
        setAmmo(ammo);
        setMoney(money);
        setScore(score);

        if (UnityServiceManager.GetKeyDown(KeyCode.I))
        {
           openInventory();
        }

    }
    public void setHealth(int health)
    {
        if(health <= 0) healthView.value = 0;
        else if(health >=100) healthView.value = 100;
        else healthView.value = health;

        if(health <= 50 && health > 30) healthFill.color = Color.yellow;
        else if (health <=30 ) healthFill.color = Color.red;
        else healthFill.color = Color.white;
    }

    public void setScore(int score)
    {
        if (score <= 0) scoreView.text = formatValue(0);
        else scoreView.text = formatValue(score);
    }
    public void setMoney(int money)
    {
        if (money <= 0) moneyView.text = formatValue(0);
        else moneyView.text = formatValue(money);
    }
    public void setAmmo(int ammo)
    {
        if (ammo <= 0) ammoView.text = formatValue(0);
        else ammoView.text = formatValue(ammo);
    }

    public void openInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    private string formatValue(int value)
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
