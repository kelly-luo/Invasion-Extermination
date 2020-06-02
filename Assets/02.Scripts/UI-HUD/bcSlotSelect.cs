using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class bcSlotSelect : ButtonClicked
{
    public InventoryManager inventoryManager;
    public int InstanceId { get; set; } = -1;


    public GameObject slot_image;
    public TMP_Text stack_text;
    public override void ButtonEvent(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            inventoryManager.SetPrimary(InstanceId);
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryManager.SetSecondary(InstanceId);
        }
    }

    public void setSprite(Sprite sprite)
    {
        slot_image.GetComponent < Image >().sprite = sprite;
        slot_image.SetActive(true);
    }

    public override void ButtonHover(PointerEventData eventData)
    {
        inventoryManager.DisplayHoverPanel(InstanceId);
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
        inventoryManager.HideHoverPanel();
    }
}
