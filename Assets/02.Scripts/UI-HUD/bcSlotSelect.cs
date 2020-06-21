/*bcSlotSelect MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user wants to select the the gun in their inventory
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  10.05.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//Text Mesh Pro support package 
using TMPro;

public class bcSlotSelect : ButtonClicked
{
    public InventoryManager inventoryManager;
    public int InstanceId { get; set; } = -1;

    public GameObject slot_image;
    public TMP_Text stack_text;

    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when a button is clicked on. When the user presses left mouse click, make gun primary
    *  else if right mouse click, make gun secondary
    */
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
    /*
    * SetSprite()
    *  ~~~~~~~~~~~~~~~~
    *  Sets the inventory slot gun sprite
    */
    public void SetSprite(Sprite sprite)
    {
        slot_image.GetComponent <Image>().sprite = sprite;
        slot_image.SetActive(true);
    }
    /*
    * ButtonHover()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when the mouse hovers over a button. Displays the guns information in the hover panel
    */
    public override void ButtonHover(PointerEventData eventData)
    {
        inventoryManager.DisplayHoverPanel(InstanceId);
    }
    /*
    * ButtonHoverExit()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when the mouse hovers over a button. Hides the gun hover panel
    */
    public override void ButtonHoverExit(PointerEventData eventData)
    {
        inventoryManager.HideHoverPanel();
    }
}
