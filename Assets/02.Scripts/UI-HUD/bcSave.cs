using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcSave : ButtonClicked
{
    [SerializeField] private PlayerInformation playerInformation;
    [SerializeField] private TMP_Text saveLoadText;
    public override void ButtonEvent(PointerEventData eventData)
    {
        playerInformation.SavePlayer();
    }

    public override void ButtonHover(PointerEventData eventData)
    {

    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }

    public void DisplaySaveLoadText()
    {
        //float startTime = UnityService.DeltaTime;
        //saveLoadText.gameObject.SetActive(false);
    }
}
