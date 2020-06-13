using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    
    public GameObject[] slots;
    void Start()
    {
        GameObject inventory = this.gameObject;
        int count = inventory.transform.childCount;
        slots = new GameObject[count];
        for(int i = 0;i < count; i++)
        {
            slots[i] = inventory.transform.GetChild(i).gameObject;
        }
    }
}
