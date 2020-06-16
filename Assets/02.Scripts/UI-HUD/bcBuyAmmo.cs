using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcBuyAmmo : ButtonClicked
{
    public ShopManager manager;
    public override void ButtonEvent(PointerEventData eventData)
    {
        manager.BuyAmmo();
    }

    public override void ButtonHover(PointerEventData eventData)
    {

    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }
}
