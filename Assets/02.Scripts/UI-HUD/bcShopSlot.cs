using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bcShopSlot : ButtonClicked
{
    public ShopManager manager;
    //public ShopItem shopItem;
    public int index = -1;
    [SerializeField] private Image image;

    public override void ButtonEvent(PointerEventData eventData)
    {
        if(index != -1)manager.MakeGunPanel(index);
    }

    public void SetSlot(Sprite sprite,int index)
    {
        this.index = index;
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
