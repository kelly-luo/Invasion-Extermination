﻿//
// bcSave HUD SAVE BUTTON
// ~~~~~~~~~~~~~~~~~~~~~~
// This is the functionality for the save button on the HUD when clicked.
// Initiating to start saving player information and display the corresponding text from save success or not.
// 
// AUT University - 2020 - Yuki Liyanage & Kelly Luo
// 
// Revision History
// ~~~~~~~~~~~~~~~~
// 22.05.2020 Creation date (Yuki)
// 02.06.2020 Added more functionality to MenuButton (Yuki)
// 7.06.2020 Added displaying save text for 3 seconds (Kelly)

//
// .NET support packages
// ~~~~~~~~~~~~~~~~~~~~~
using System.Collections;
//
// Unity support packages
// ~~~~~~~~~~~~~~~~~~~~~~
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcSave : ButtonClicked
{
    [SerializeField] public UIManager manager;
    [HideInInspector]
    public PlayerInformation playerInformation;
    [SerializeField] public TMP_Text saveLoadText;


    private void Start()
    {
        playerInformation = manager.playerInformation;
    }
    //
    // ButtonEvent()
    // ~~~~~~~~~~~~~
    // Initiates player information to be saved and then start timed display of the correct save text afterwards
    //
    // eventData   Button click incoming event
    //

    public override void ButtonEvent(PointerEventData eventData)
    {
        bool successful = playerInformation.SavePlayer();
        this.DisplaySaveLoadText(successful);
    }

    public override void ButtonHover(PointerEventData eventData)
    {

    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }

    //
    // DisplaySaveLoadText()
    // ~~~~~~~~~~~~~~~~~~~~~
    // Enables and displays the appropriate text and colour depending if the save was successful or not.
    // Also starts a 3 second Unity Coroutine.
    //
    // saveSuccess   Whether the saving of the player information was success in the SaveSystem
    //
    private void DisplaySaveLoadText(bool saveSuccess)
    {
        if (saveSuccess)
        {
            saveLoadText.SetText("Save complete");
            saveLoadText.color = Color.green;
        }
        else
        {
            saveLoadText.SetText("Save unsuccessful");
            saveLoadText.color = Color.red;
        }

        saveLoadText.gameObject.SetActive(true);

        StartCoroutine(this.timerWait());
    }

    //
    // timerWait()
    // ~~~~~~~~~~~
    // Waits for 3 seconds and then disables the save completed/incompleted text
    //
    // returns      IEnumerator for Coroutine
    //
    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(3f);
        saveLoadText.gameObject.SetActive(false);
    }
}
