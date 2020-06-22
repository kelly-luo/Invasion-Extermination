/*SetVolume Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the changing volume
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  18.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Toggle mute_toggle;

    private float currentVol = 1f;
    public float CurrentVol
    {
        get { return currentVol; }
        set { currentVol = value; }
    }
    private Slider slider;
    public Slider Slider
    {
        get { return slider; }
        set { slider = value; }
    }
    private bool mute = false;
    public bool Mute
    {
        get { return mute; }
    }
    private const float ZEROVAL = 0.0001f;
    private const string VOLNAME = "GameVol";


    void Start()
    {
        Initialize();
    }
    /*
    * Initialize()
    *  ~~~~~~~~~~~~~~~~
    *  Initialize the Volume class, and add a listener whenever slider value changes
    */
    private void Initialize()
    {
        mute_toggle.onValueChanged.AddListener(delegate
        {
            SetMute(mute_toggle.isOn);
        });
        slider = GetComponent<Slider>();
        SetLevel(currentVol);
    }
    /*
    * SetLevel()
    *  ~~~~~~~~~~~~~~~~
    *  Set Volume levels
    */
    public void SetLevel (float SliderVal)
    {
        if (SliderVal > 1f) SliderVal = 1f;
        else if (SliderVal < 0.0001f) SliderVal = 0.0001f;

        currentVol = SliderVal;
        if (slider != null) slider.value = currentVol;
        if (!mute && mixer != null) mixer.SetFloat(VOLNAME, VolumeLog(currentVol));
    }
    /*
    * SetMute()
    *  ~~~~~~~~~~~~~~~~
    *  Mute the volume
    */
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
            SetLevel(slider.value);
        }
    }
    /*
    * VolumeLog()
    *  ~~~~~~~~~~~~~~~~
    *  Using logarithims to make the sound decrease and increase nicely, rather than harshly
    */
    private float VolumeLog(float SliderVal)
    {
        return Mathf.Log10(SliderVal) * 20;
    }

    


}
