/*InventorySlots Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class gets all slots connected 
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  12.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  System support packages
 */
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    
    public GameObject[] slots;
    /*
    * Start()
    *  ~~~~~~~~~~~~~~~~
    *  Gets all the gameobjects connected to this gameobjects
    */
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
