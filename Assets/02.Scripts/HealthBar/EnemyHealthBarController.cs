using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas uiCanvas;

    private RectTransform rectParent;
    private RectTransform rectHp;

    [HideInInspector]
    public GameObject healthBar;

    [HideInInspector]
    public Vector3 offset = Vector3.zero;

    [HideInInspector]
    public Transform targetTr;

    private const float viewdistance = 50.0f;
    private float dist;

    void Start()
    {
        uiCanvas = GetComponentInParent<Canvas>();
        uiCamera = uiCanvas.worldCamera;
        rectParent = uiCanvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        UpdateHealthBarPosition();
    }

    public void UpdateHealthBarPosition()
    {
        var screenpos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        if (screenpos.z < 0.0f) screenpos *= -1.0f;

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenpos, uiCamera, out localPos);

        rectHp.localPosition = localPos;
        healthVisible(targetTr.position);
    }

    private void healthVisible(Vector3 target)
    {
        dist = Vector3.Distance(target, Camera.main.transform.position);
        if (dist > viewdistance)
            rectHp.transform.localScale = Vector3.zero;
        else
            rectHp.transform.localScale = Vector3.one;
    }

}
