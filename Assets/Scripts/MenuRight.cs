using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MenuRight : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (MapSelect.index < MapSelect.size) MapSelect.index += 1;
  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}

