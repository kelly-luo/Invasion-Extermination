using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunPanel : MonoBehaviour
{
    [SerializeField] private Image gunImage;
    [SerializeField] private TMP_Text gunname;

    [SerializeField] private TMP_Text gundamage;
    [SerializeField] private TMP_Text gunclipsize;
    [SerializeField] private TMP_Text gunprice;

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
