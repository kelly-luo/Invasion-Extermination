using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClicked : MonoBehaviour
{
    private MyMenuButton menuButton;
    public GameObject frame;
    public GameObject inView;
    // Start is called before the first frame update
    void Start()
    {
        menuButton = GetComponent<MyMenuButton>();
    }

    // Update is called once per frame
    void Update()
    {

        if(menuButton.name == "Exit")
        {
            Application.Quit();
        }

        if (menuButton.clicked)
        {
            GobalFrame.bringToView(frame,menuButton);
        }
        else
        {
            GobalFrame.leaveView(frame);
        }

        inView = GobalFrame.frameInView;

    }
}
