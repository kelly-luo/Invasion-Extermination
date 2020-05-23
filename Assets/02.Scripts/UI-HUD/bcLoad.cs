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

}
