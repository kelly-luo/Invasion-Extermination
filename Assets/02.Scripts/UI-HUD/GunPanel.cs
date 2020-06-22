/*GunPanel Class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This handles displaying information on the gun panel in the shop
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
//Tezt mesh pro support package
using TMPro;

public class GunPanel : MonoBehaviour
{
    [SerializeField] private Image gunImage;
    [SerializeField] private TMP_Text gunname;

    [SerializeField] private TMP_Text gundamage;
    [SerializeField] private TMP_Text gunclipsize;
    [SerializeField] private TMP_Text gunprice;

    //These classes displays the information onto the Gun Panel
    public void SetImage(Sprite sprite)
    {
        gunImage.sprite = sprite;
    }
    public void SetGunName(string text)
    {
        gunname.text = text;
    }
    public void SetGunDamage(int text)
    {
        gundamage.text = UIManager.FormatValue(text);
    }
    public void SetGunClipSize(int text)
    {
        gunclipsize.text = UIManager.FormatValue(text);
    }
    public void SetGunPrice(int text)
    {
        gunprice.text = UIManager.FormatValue(text);
    }

}
