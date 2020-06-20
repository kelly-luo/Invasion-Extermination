/*bcApplySettings MenuButton
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the inputs that the user wants as their settings, and applies it
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
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bcApplySettings : ButtonClicked
{
    public Slider volume;
    public Slider MouseY;
    public Slider MouseX;

    public Toggle mutetg;
    public Dropdown resoultionCombo;
    public Toggle windowToggleMode;
    public Toggle invertMousetg;

    public CameraControl cameraControl;
    public bcShowPanel button;

    public AudioMixer mixer;


    private float currentVol = 1f;
    public float CurrentVol
    {
        get { return currentVol; }
        set { currentVol = value; }
    }

    private bool mute = false;
    public bool Mute
    {
        get { return mute; }
    }
    private const float ZEROVAL = 0.0001f;
    private const string VOLNAME = "GameVol";


    /*
   * ButtonEvent()
   *  ~~~~~~~~~~~~~~~~
   *  Handles when a button is clicked on. It takes the user's input and change the settings of the game
   *  on the user inputs.
   */
    public override void ButtonEvent(PointerEventData eventData)
    {
        

        bool windowsMode = windowToggleMode.isOn;
        string restext = resoultionCombo.options[resoultionCombo.value].text;
        string[] restextSp = restext.Split('x');

        int width = int.Parse(restextSp[0]);
        int height = int.Parse(restextSp[1]);

        SetLevel(volume.value);
        SetMute(mutetg.isOn);

        cameraControl.InvertMouse(invertMousetg.isOn);
        cameraControl.SetMouseY(MouseY.value);
        cameraControl.SetMouseX(MouseX.value);

        Screen.SetResolution(width, height, windowsMode);

        if(button != null) button.ButtonEvent(null);
    }

    public void SetLevel(float SliderVal)
    {
        if (SliderVal > 1f) SliderVal = 1f;
        else if (SliderVal < 0.0001f) SliderVal = 0.0001f;

        currentVol = SliderVal;
        if (volume != null) volume.value = currentVol;
        if (!mute && mixer != null) mixer.SetFloat(VOLNAME, VolumeLog(currentVol));
    }
    public void SetMute(bool mute)
    {
        if (mute)
        {
            SetLevel(ZEROVAL);
            this.mute = mute;
        }
        else
        {
            this.mute = mute;
            SetLevel(volume.value);
        }
    }

    private float VolumeLog(float SliderVal)
    {
        return Mathf.Log10(SliderVal) * 20;
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
