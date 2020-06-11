using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bcShopSlot : ButtonClicked
{
    public ShopManager manager;
    public ShopItem shopItem;
    [SerializeField] private Image image;

    public override void ButtonEvent(PointerEventData eventData)
    {
        if(shopItem != null)manager.MakeGunPanel(shopItem);
    }

    public void SetSlot(Sprite sprite,ShopItem shopItem)
    {
        this.shopItem = shopItem;
        if (!image.gameObject.activeSelf) image.gameObject.SetActive(true);
        image.sprite = sprite;
    }

    public override void ButtonHover(PointerEventData eventData)
    {
        
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
     
    }

}
