using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverPanelValues : MonoBehaviour
{
    public TMP_Text[] textvalues;
    public TMP_Text gunName;
    public TMP_Text selectedText;
    public Image selectedImage;
    public Image gunImage;


    public int getSize()
    {
        return textvalues.Length;
    }

    public void setImage(Sprite sprite)
    {
        gunImage.sprite = sprite;
    }

    public void setTitle(string name)
    {
        gunName.text = name;
    }

    public void setSelected(string selected)
    {
        if (!selected .Equals("Not Selected")) selectedImage.color = new Color(0f, 1f,0.090f, 0.392f);
        else selectedImage.color = new Color(1f, 0, 0, 0.392f);
        selectedText.text = selected;
    }

    public void setText(int index,float value)
    {
        textvalues[index].text = UIManager.FormatValue((int)value);
    }
}
