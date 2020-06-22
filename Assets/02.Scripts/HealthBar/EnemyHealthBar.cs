/*EnemyHealthBar class
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
 * This class handles the enmey health bars
 * 
 * AUT University - 2020 - Yuki Liyanage
 * 
 * Revision History
 *  ~~~~~~~~~~~~~~~~
 *  10.06.2020 Creation date (Yuki)
 *  21.06.2020 Refactored, and removed unnecessary code (Yuki)
 *  
 *  
 * FinteStateMachine
 */
using IEGame.FiniteStateMachine;
// Unity Engine Support package
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
    /*
    * onDamage()
    *  ~~~~~~~~~~~~~~~~
    *  When the Monster takes damage then display that change on the UI
    */
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
    /*
    * SetHealthBar()
    *  ~~~~~~~~~~~~~~~~
    *  Set-Up Health bar UI
    */
    public void SetHealthBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();

        
        GameObject healthbarObject = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpSlider = healthbarObject.GetComponent<Slider>();

        var healthcontroller = healthbarObject.GetComponent<EnemyHealthBarController>();
        healthcontroller.targetTr = this.gameObject.transform;
        healthcontroller.offset = hpBarOffset;
        
    }
}
