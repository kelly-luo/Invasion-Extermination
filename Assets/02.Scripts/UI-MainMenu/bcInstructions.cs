/*bcInstructions MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This button handles when the user clicks to see the next instruction image. This class just cycles through each image
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  19.04.2020 Creation date (Yuki)
 *  12.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  Unity support packages
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class bcInstructions : ButtonClicked
{
    public int CurrentIndex { get; set; }
    public int[] InternalImageID { get;  set; }
    public Sprite[] images;
    public Image image;
    /*
     * Start()
     *  ~~~~~~~~~~~~~~~~
     *  Initilizes the button. Makes the image start with the 0 index sprite
     */
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
    /*
     * ButtonEvent()
     *  ~~~~~~~~~~~~~~~~
     *  Handles the button click. Goes to the next index, if index is greater than the images size, it goes back to one,, making it constanly cycle
     */
    public override void ButtonEvent(PointerEventData eventData)
    {
        CurrentIndex++;
        if (CurrentIndex == getSize()) CurrentIndex = 0;
        if(images != null) image.sprite = images[CurrentIndex];
    }
    /*
     * getSize()
     *  ~~~~~~~~~~~~~~~~
     *  Gets the size of the images array
     */
    public int getSize()
    {
        if (images == null) return InternalImageID.Length;
        return images.Length;
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
