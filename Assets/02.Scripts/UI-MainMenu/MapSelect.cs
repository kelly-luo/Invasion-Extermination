using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{

    public Image imageComp;

    public MapClass currentMap;

    public int size;
    public int index;

    private int currentIndex;
    public MapClass[] maps;

    public string hmm;
  
    void Start()
    {
        index = 0;
        size = maps.Length;

        updatePanel();
    }
    void Update()
    {
        if(currentIndex != index)
        {
            if (index < 0) index = size - 1;
            else if (index > size - 1) index = 0;
            updatePanel();
        }

    }

    private void updatePanel()
    {
        currentMap = maps[index];
        imageComp.sprite = currentMap.mapImage;
        currentIndex = index;

    }

    public string getMap()
    {
        return currentMap.mapID;
    }

}
