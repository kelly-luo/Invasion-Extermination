/*bcMainMenu MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user clicks on any of the main menu buttons in the beginning of the game
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  21.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;
using UnityEngine.EventSystems;

public class bcMainMenu : ButtonClicked
{
    private MenuButton menuButton;
    public GameObject frame;
    public GameObject inView;

    /*
     * start()
     *  ~~~~~~~~~~~~~~~~
     * Initializes bcMainMenu, by getting the menuButton component
     */
    void Start()
    {
        menuButton = GetComponent<MenuButton>();
    }
    /*
     * ButtonEvent()
     *  ~~~~~~~~~~~~~~~~
     *  Handles the button click. Brings the panel that is attached to this button to view, and any other views that
     *  was originally being viewed to leave.
     */
    public override void ButtonEvent(PointerEventData eventData)
    {

        if (frame.activeSelf == false)
        {
            GobalFrame.BringToView(frame, menuButton);
        }
        else
        {
            GobalFrame.LeaveView(frame);
        }

        inView = GobalFrame.frameInView;
    }
    /*
     * getInView()
     *  ~~~~~~~~~~~~~~~~
     *  get the current panel that is view to the player
     */
    public GameObject GetInView()
    {
        return inView;
    }
    /*
     * setFrame()
     *  ~~~~~~~~~~~~~~~~
     *  Set the frame(panel) that is attached to this button
     */
    public void SetFrame(GameObject frame)
    {
        this.frame = frame;
    }

    #region UNUSED_BUTTON_CLICK_METHODS
    public override void ButtonHover(PointerEventData eventData)
    {

    }

    public override void ButtonHoverExit(PointerEventData eventData)
    {

    }
    #endregion
}
