using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{

    public Image imageComp;

    public Sprite currentMapImage;
    public string currentMapID;
    public int internalIndex = 0;

    public static int size;

    public static int index;

    public MapClass[] maps;

    // Start is called before the first frame update
    void Start()
    {
       

        size = maps.Length;
        index = internalIndex;
        currentMapID = maps[0].mapID;
        currentMapImage = maps[0].mapImage;

        imageComp.sprite = currentMapImage;
    }

    // Update is called once per frame
    void Update()
    {
        if(internalIndex != index)
        {
            internalIndex = index;
            currentMapID = maps[internalIndex].mapID;
            currentMapImage = maps[internalIndex].mapImage;

            imageComp.sprite = currentMapImage;
        }
    }

}
