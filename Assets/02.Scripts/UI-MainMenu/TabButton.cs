/*TabButton UI Custom Button 
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class is a custom button class, it handles clicks but when a it is clicked
 * it stays clicked. It also handles animations 
 *
 * 
 * AUT University - 2020 - Yuki Liyanage
 * Used Tutorial: https://www.youtube.com/watch?v=211t6r12XPQ - Game Dev Guide
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  21.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed uncessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler,IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    /*
     * OnPointerClick()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when tab button is click, it activates the tab group function, to select it
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    /*
     * OnPointerEnter()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when the mouse is hovering over the tab button. It calls the tab group function.
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }
    /*
     * OnPointerExit()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when the mouse is hovering leaves the button. It calls the tab group function. 
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
    /*
     * Start()
     *  ~~~~~~~~~~~~~~~~
     *  Intializes the tab button
     */
    void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    /*
     * Select()
     *  ~~~~~~~~~~~~~~~~
     *  When the tab button is selected, it invokes a tab selected. It displays the tab button selected.
     */
    public void Select()
    {
        if(onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }
    /*
     * Deselect()
     *  ~~~~~~~~~~~~~~~~
     *  When the tab button is deselected, it invokes a tab deselected. It stop displaying the tab button as selected.
     */
    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
