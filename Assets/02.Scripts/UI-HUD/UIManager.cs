﻿/*UIManager class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the UI of the hud
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  11.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  22.06.2020 Add control panel disappearing after a set time (Yuki)
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.UI;
// Text mesh Pro support system package
using TMPro;
//System support pacakge
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject Controls;

    public GameObject healthObject;
    public Slider healthView;
    public Image healthFill;

    public Slider scoreProgress;
    public TMP_Text roundView;
    public RoundPopUp roundPopUp;
    public TMP_Text scoreView;
    public TMP_Text reqScoreView;

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
    public int displayreqscore;
    public int displaygunammo;
    public int displayamountammo;
    public int displayhealth;


    public IUnityServiceManager UnityService { get; set; } = UnityServiceManager.Instance;


    private const int LOWHEALTH = 30;
    private const int MEDIUMHEALTH = 50;
    private const int MAXHEALTH = 100;

    void Start()
    {
        Initialize();
        StartCoroutine(this.timerWait());
    }
    /*
    * Initialize()
    *  ~~~~~~~~~~~~~~~~
    *  Initialize the GUI, and sets up everything
    */
    public void Initialize()
    {

        if (roundView != null) roundView.text = FormatValue(gameManager.RoundNo);
        if (reqScoreView != null) reqScoreView.text = FormatValue(gameManager.RequiredScore);

        if (playerInformation != null)
        {
            SetHealth(playerInformation.Health);
            SetAmountAmmo(playerInformation.Ammo);
            SetMoney(playerInformation.Money);
            SetScore(playerInformation.Score);
        }

        invManager.Initialize(playerInformation.PlayerInventory);
        invManager.UpdateWeaponSlots();

        if (shopManager != null) VisibleOnScreen(shopManager.gameObject);
        if (GameMenuPanel != null) VisibleOnScreen(GameMenuPanel);
        if (invManager.inventoryPanel != null) VisibleOnScreen(invManager.inventoryPanel);
    }
    /*
    * Update()
    *  ~~~~~~~~~~~~~~~~
    *  Updates display information when internal information changes
    */
    void Update()
    {
        if (displayhealth != Convert.ToInt32(playerInformation.Health)) SetHealth(playerInformation.Health);
        if (displaymoney != playerInformation.Money) SetMoney(playerInformation.Money);
        if (displaygunammo != playerInformation.PlayerInventory.Equppied().NumOfBullet) SetGunAmmo(playerInformation.PlayerInventory.Equppied().NumOfBullet);
        if (displayamountammo != playerInformation.Ammo) SetAmountAmmo(playerInformation.Ammo);
        if (displayscore != playerInformation.Score) SetScore(playerInformation.Score);
        if (displayreqscore != gameManager.RequiredScore) SetReqScore(gameManager.RequiredScore);

        CheckInput();
    }
    /*
    * CheckInput()
    *  ~~~~~~~~~~~~~~~~
    *  Handles User's inputs, and handles what GUI to display or hide depending on the user input
    */
    public void CheckInput()
    {

        if (UnityService.GetKeyUp(KeyCode.I))
        {
            //Toggle inventory panel (Only if shop is not active)
            if (invManager.inventoryPanel != null && !IsVisible(shopManager.gameObject) && !IsVisible(GameMenuPanel)) 
                VisibleOnScreen(invManager.inventoryPanel);
        }

        if (UnityService.GetKeyUp(KeyCode.B))
        {
            if (!IsVisible(GameMenuPanel))
            {
                //Toggle Shop panel
                if (!IsVisible(shopManager.gameObject))
                {
                    MakeVisible(invManager.inventoryPanel, shopManager.gameObject);
                    MakeInvisble(healthObject);
                }
                else
                {
                    MakeInvisble(invManager.inventoryPanel, shopManager.gameObject);
                    MakeVisible(healthObject);
                }
            }

        }

        if (UnityService.GetKeyUp(KeyCode.Escape))
        {
            //Toggle game menu panel
            if (!IsVisible(GameMenuPanel))
            {
                MakeVisible(GameMenuPanel);
                MakeInvisble(invManager.inventoryPanel, shopManager.gameObject, healthObject, Controls);
            }
            else
            {
                MakeInvisble(GameMenuPanel);
                MakeVisible(healthObject);
            }

        }

        //Change to player's primary weapon
        if (UnityService.GetKeyUp(KeyCode.Alpha1))
        {
            if (invManager.PlayerInventory != null) invManager.PlayerInventory.SelectPrimary();
        }

        //Change to player's secondary weapon
        if (UnityService.GetKeyUp(KeyCode.Alpha2))
        {
            if (invManager.PlayerInventory != null) invManager.PlayerInventory.SelectSecondary();
        }
    }



    #region Displaying_Information
    //Displays and show UI information
    public void SetScore(int score)
    {
        if (score <= 0) displayscore = 0;
        else displayscore = score;

        if (displayscore >= displayreqscore && displayreqscore != 0)
        {

            if (!gameManager.bossRound)
            {
                roundPopUp.playAnimation();
                gameManager.ClearRound = true;

                if (roundView != null) roundView.text = FormatValue(gameManager.RoundNo);
                if (reqScoreView != null) reqScoreView.text = FormatValue(gameManager.RequiredScore);
            }

        }

        if (scoreView != null) scoreView.text = FormatValue(displayscore);
        if (scoreProgress != null && !gameManager.bossRound) scoreProgress.value = (float)displayscore / (float)gameManager.RequiredScore;
        else if (scoreProgress != null) scoreProgress.value = 1f;
    }

    public void SetReqScore(int reqScore)
    {
        displayreqscore = reqScore;
        if (reqScoreView != null) reqScoreView.text = FormatValue(reqScore);
    }


    public void SetMoney(int money)
    {
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
        else if (health >= MAXHEALTH) displayhealth = MAXHEALTH;
        else displayhealth = health;

        if (healthView != null && healthFill != null)
        {
            if (displayhealth <= MEDIUMHEALTH && displayhealth > LOWHEALTH) healthFill.color = Color.yellow;
            else if (displayhealth <= LOWHEALTH) healthFill.color = Color.red;
            else healthFill.color = Color.white;
            healthView.value = displayhealth;
        }

    }

    #endregion
    #region Managing_UI
    //Static functions to handle/format UI
    static public string FormatValue(int value)
    {
        //When digit number is greater than double digit
        if(value < 10)
        {
            return "0" + value;
        }
        else
        {
            return ""+ value;
        }

    }

    static public void MakeInvisble(params GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (IsVisible(panel)) VisibleOnScreen(panel);
        }
    }
    static public void MakeVisible(params GameObject[] panels)
    {
        foreach (GameObject panel in panels)
        {
            if (!IsVisible(panel)) VisibleOnScreen(panel);
        }
    }
    //Toggles visibility of gameobject (panel)
    static public void VisibleOnScreen(GameObject panel)
    {
        if (panel.transform.localScale.x == 1)
        {
            panel.transform.localScale = Vector3.zero;
        }
        else
        {
            panel.transform.localScale = Vector3.one;
        }
    }

    static public bool IsVisible(GameObject panel)
    {
        if (panel.transform.localScale.x == 1)return true;
        else return false;
        
    }

    //
    // timerWait()
    // ~~~~~~~~~~~
    // Waits for 5 seconds and hides controls
    //
    // returns      IEnumerator for Coroutine
    //
    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(5f);
        VisibleOnScreen(Controls);
    }

    #endregion
}
