using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class bcMenuRight : ButtonClicked
{
    public MapSelect mapSelect;
    public override void ButtonEvent(PointerEventData eventData)
    {
         mapSelect.index += 1;
    }

    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
       
    }
}

