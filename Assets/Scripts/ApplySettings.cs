using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplySettings : MonoBehaviour
{
    private MyMenuButton menuButton;
    public Dropdown resoultionCombo;
    public Toggle windowToggleMode;
    public string hmm;
    // Start is called before the first frame update
    void Start()
    {
        menuButton = GetComponent<MyMenuButton>();
    }

    // Update is called once per frame
    void Update()
    {
        bool windowsMode = windowToggleMode.isOn;
        string restext = resoultionCombo.options[resoultionCombo.value].text;
        string[] restextSp = restext.Split('x');

        int width = int.Parse(restextSp[0]);
        int height = int.Parse(restextSp[1]);


        if (menuButton.clicked)
        {
            Screen.SetResolution(width, height, true);
            hmm = "" + Screen.height;
        }
        else
        {

        }

    }
}
