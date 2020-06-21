/*bcShopSlot MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user wants to select a gun to view
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  11.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code & added Hovering functions to buttons (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bcShopSlot : ButtonClicked
{
    public ShopManager manager;
    public int index = -1;
    [SerializeField] private Image image;
    /*
    * ButtonEvent()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when a button is clicked on. Changes the view panel to display the gun information that is selected
    */
    public override void ButtonEvent(PointerEventData eventData)
    {
        if(index != -1)manager.MakeGunPanel(index,true);
    }
    /*
    * SetSlot()
    *  ~~~~~~~~~~~~~~~~
    *  Sets the gun sprite for this slot
    */
    public void SetSlot(Sprite sprite,int index)
    {
        this.index = index;
        if (!image.gameObject.activeSelf) image.gameObject.SetActive(true);
        image.sprite = sprite;
    }
    /*
    * ButtonHover()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when the mouse hovers over a button. Changes the view panel to display the gun information that is being hovered over
    */
    public override void ButtonHover(PointerEventData eventData)
    {
        if (index != -1) manager.MakeGunPanel(index,false);
    }
    /*
    * ButtonHoverExit()
    *  ~~~~~~~~~~~~~~~~
    *  Handles when the mouse hovers over a button. Reset the view panel to display the gun information that is was currently selected
    */
    public override void ButtonHoverExit(PointerEventData eventData)
    {
        if (manager.selectedindex != -1) manager.MakeGunPanel(manager.selectedindex,false);
    }
  

}
