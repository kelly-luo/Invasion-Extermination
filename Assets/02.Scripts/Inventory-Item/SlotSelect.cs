using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotSelect : ButtonClicked
{
    public InventoryManager inventoryManager;

    public override void ButtonEvent(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryManager.SetPrimary(transform.GetSiblingIndex());
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryManager.SetSecondary(transform.GetSiblingIndex());
        }
    }
}
