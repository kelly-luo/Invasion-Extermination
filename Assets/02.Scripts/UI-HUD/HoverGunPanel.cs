/*HoverPanelValues Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This handles displaying information on the gun panel in the hover panel
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  11.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 *  UnityEngine support packages
 */
using UnityEngine;
using UnityEngine.UI;
//Text mesh pro support package
using TMPro;

public class HoverGunPanel : MonoBehaviour
{
    public TMP_Text[] textvalues;
    public TMP_Text gunName;
    public TMP_Text selectedText;
    public Image selectedImage;
    public Image gunImage;

    //These classes displays the information onto the hover Panel
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
