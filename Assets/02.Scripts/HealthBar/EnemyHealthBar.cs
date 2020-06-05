using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private float displayHp = 1.0f;
    private float initHp = 100.0f;

   [SerializeField] public GameObject hpBarPrefab;
    //offset of enemy's hp bar
    public Vector3 hpBarOffset = new Vector3(0.0f, 2.2f, 0.0f);
    //Parent canvas
    private Canvas uiCanvas;
    //image for the fill amount in the health slider
    private Slider hpSlider;

    public void onDamage(float currentHealth)
    {
        displayHp = currentHealth / initHp;
        hpSlider.value = displayHp;

        if(displayHp <= 0.0f)
        {
            hpSlider.gameObject.SetActive(false);
            Destroy(hpSlider);
        }
    }
    public void SetHPBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        
        GameObject healthbarObject = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpSlider = healthbarObject.GetComponent<Slider>();

        var healthcontroller = healthbarObject.GetComponent<EnemyHealthBarController>();
        healthcontroller.targetTr = this.gameObject.transform;
        healthcontroller.healthBar = healthbarObject;
        healthcontroller.offset = hpBarOffset;
        
    }
}
