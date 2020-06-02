using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcShowPanel : ButtonClicked
{
    public GameObject nextPanel;
    public GameObject currentPanel;
    public override void ButtonEvent(PointerEventData eventData)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
}
