using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcClosePanel : ButtonClicked
{
    public GameObject panel;
    public override void ButtonEvent(PointerEventData eventData)
    {
        if (panel != null) panel.SetActive(false);
    }

    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
       
    }
}
