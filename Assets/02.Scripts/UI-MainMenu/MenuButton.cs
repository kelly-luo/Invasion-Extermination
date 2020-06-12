/*MenuButton UI Custom Button 
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class is a custom button class, it handles the animations for the button &
 * and gives more functionality & flexibility than a regular unity button
 *
 * 
 * AUT University - 2020 - Yuki Liyanage
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
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    /*
     * State
     *  ~~~~~~~~~~~~~~~~
     *  What state the button is currently in
     */
    public enum State
    {
        selected,
        deselected,
        pressed,
        idle
    }

    public Animator animator;

    public State currentState;

    public bool pressed = false;

    public ButtonClicked buttonClicked;

    public bool enable { get; set; } = true;

    #region MonoBehaviour_Functions
    /*
     * Start()
     *  ~~~~~~~~~~~~~~~~
     *  Initilizes the button
     */
    void Start()
    {
        currentState = State.idle;
        buttonClicked = GetComponent<ButtonClicked>();
    }
    /*
     * Update()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when the button has been pressed, then change state back to normal
     */
    void Update()
    {

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pressed") && pressed)
        {

            animator.SetBool("Pressed", false);
            pressed = false;
        }
    }
    #endregion

    #region ButtonClick
    /*
     * getButtonClicked()
     *  ~~~~~~~~~~~~~~~~
     *  Gets the ButtonClicked clas attach to this MenuButton
     */
    public ButtonClicked getButtonClicked()
    {
        return buttonClicked;
    }

    /*
    * setButtonClicked()
    *  ~~~~~~~~~~~~~~~~
    *  Sets the ButtonClicked class
    */
    public void SetButtonClicked(ButtonClicked buttonClicked)
    {
        this.buttonClicked = buttonClicked;
    }
    #endregion

    #region Handling_User_Input
    /*
     * OnPointerClick()
     *  ~~~~~~~~~~~~~~~~
     * Handles when the button has been clicked
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        if (enable)
        {
            if (animator != null) animator.SetBool("Pressed", true);
            pressed = true;
            Click(eventData);
        }

    }
    /*
     * OnPointerEnter()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when the mouse is hovering over the button
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enable)
        {
            if (animator != null) animator.SetBool("Selected", true);
            Hover(eventData);
        }
     
    }
    /*
     * OnPointerExit()
     *  ~~~~~~~~~~~~~~~~
     *  Handles when the mouse is leaves hovering over the button
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        if (enable)
        {
            if (animator != null) animator.SetBool("Selected", false);
            HoverExit(eventData);
        }
      
    }
    /*
     * Click()
     *  ~~~~~~~~~~~~~~~~
     * Handles a button click (If button is disabled, don't activate buttonClicked function)
     */
    public void Click(PointerEventData eventData)
    {
        if (enable)
        {
            buttonClicked.ButtonEvent(eventData);
        }
    
    }
    /*
     * Hover()
     *  ~~~~~~~~~~~~~~~~
     *  Handles a button when being hovered over (If button is disabled, don't activate buttonClicked function)
     */
    public void Hover(PointerEventData eventData)
    {
        if (enable)
        {
            buttonClicked.ButtonHover(eventData);
        }

    }
    /*
     * HoverExit()
     *  ~~~~~~~~~~~~~~~~
     *  Handles a button when has left hover mode (If button is disabled, don't activate buttonClicked function)
     */
    public void HoverExit(PointerEventData eventData)
    {
        if (enable)
        {
            buttonClicked.ButtonHoverExit(eventData);
        }

    }
    #endregion

    #region Interactability
    /*
     * disableButton()
     *  ~~~~~~~~~~~~~~~~
     *  Disable the button, making not interactable
     */
    public void DisableButton()
    {
        enable = false;
        if(animator != null) animator.SetBool("Selected", false);
    }
    /*
     * enableButton
     *  ~~~~~~~~~~~~~~~~
     *  Enable the button, making it interactable
     */
    public void EnableButton()
    {
        enable = true;
        if (animator != null) animator.SetBool("Selected", false);
    }
    #endregion





}
