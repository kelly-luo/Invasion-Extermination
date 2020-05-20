using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDelete : ButtonClicked
{
    public InventoryManager inventoryManager;
    public GameObject Slot;
    public override void ButtonEvent(PointerEventData eventData)
    {
        inventoryManager.removeItem(Slot.transform.GetSiblingIndex());
    }
}
