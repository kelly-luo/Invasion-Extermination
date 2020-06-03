using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcLoad : ButtonClicked
{
    [SerializeField] private PlayerInformation playerInformation;
    public override void ButtonEvent(PointerEventData eventData)
    {
        playerInformation.LoadPlayer();
    }

    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
      
    }
}
