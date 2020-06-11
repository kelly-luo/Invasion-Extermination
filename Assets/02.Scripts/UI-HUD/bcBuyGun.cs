using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bcBuyGun : ButtonClicked
{
    public ShopManager manager;
    public override void ButtonEvent(PointerEventData eventData)
    {
        manager.BuyGun();
    }

    public override void ButtonHover(PointerEventData eventData)
    {

    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
 
    }


}
