using System.Collections;
using System.Collections.Generic;
using System.Timers;
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
        this.DisplaySaveLoadText();
    }

    public override void ButtonHover(PointerEventData eventData)
    {

    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }

    private void DisplaySaveLoadText()
    {
        saveLoadText.SetText("Save complete");
        saveLoadText.gameObject.SetActive(true);
        //saveLoadText..color(new Color());

        StartCoroutine(this.timerWait());
    }

    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(3f);
        saveLoadText.gameObject.SetActive(false);
    }
}
