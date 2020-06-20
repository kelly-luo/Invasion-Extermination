using IEGame.FiniteStateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private float displayHp = 1.0f;
    public float DisplayHp
    {
        get
        {
            return displayHp;
        }
    }
    private float initHp = 100.0f;

   [SerializeField] public GameObject hpBarPrefab;
    //offset of enemy's hp bar
    public Vector3 hpBarOffset = new Vector3(0.0f, 2.2f, 0.0f);
    //Parent canvas
    private Canvas uiCanvas;
    public Canvas UiCanvas
    {
        set
        {
            uiCanvas = value;
        }
    }
    //image for the fill amount in the health slider
    private Slider hpSlider;
    public Slider HpSlider
    {
        get
        {
            return hpSlider;
        }
        set
        {
            hpSlider = value;
        }
    }

    public void onDamage(ObjectStats objectStats)
    {
        if(objectStats is MonsterStats mStats)
        {
            displayHp = mStats.Health / mStats.maxHealth;
            hpSlider.value = displayHp;

            if (displayHp <= 0.0f)
            {
                hpSlider.gameObject.SetActive(false);
                hpSlider = null;
            }
        }


    }
    public void SetHPBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        
        GameObject healthbarObject = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpSlider = healthbarObject.GetComponent<Slider>();

        var healthcontroller = healthbarObject.GetComponent<EnemyHealthBarController>();
        healthcontroller.targetTr = this.gameObject.transform;
        healthcontroller.offset = hpBarOffset;
        
    }
}
