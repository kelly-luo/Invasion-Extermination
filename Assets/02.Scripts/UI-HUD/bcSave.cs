using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcSave : ButtonClicked
{
    [SerializeField] public PlayerInformation playerInformation;
    [SerializeField] public TMP_Text saveLoadText;

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

    private IEnumerator timerWait()
    {
        yield return new WaitForSeconds(3f);
        saveLoadText.gameObject.SetActive(false);
    }
}
