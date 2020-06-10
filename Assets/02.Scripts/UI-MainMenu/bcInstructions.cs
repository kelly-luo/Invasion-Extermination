using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class bcInstructions : ButtonClicked
{
    public int CurrentIndex { get; set; }
    public int[] InternalImageID { get;  set; }
    public Sprite[] images;
    public Image image;

    void Start()
    {
        CurrentIndex = 0;
        if(images != null) 
        {
        image.sprite = images[CurrentIndex];
        InternalImageID = new int[images.Length];
        for (int i = 0; i < images.Length; i++) InternalImageID[i] = i;
        }
    }
    public override void ButtonEvent(PointerEventData eventData)
    {
        CurrentIndex++;
        if (CurrentIndex == getSize()) CurrentIndex = 0;
        if(images != null) image.sprite = images[CurrentIndex];
    }

    public int getSize()
    {
        if (images == null) return InternalImageID.Length;
        return images.Length;
    }

    public override void ButtonHover(PointerEventData eventData)
    {
       
    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {
      
    }
}
