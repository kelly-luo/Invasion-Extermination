using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    public Camera uiCamera;
    public Vector3 mainCameraPosition;
    private Canvas uiCanvas;

    public RectTransform rectParent;
    public RectTransform rectHp;


    [HideInInspector]
    public IUnityServiceManager UnityServiceManager = new UnityServiceManager();

    [HideInInspector]
    public Vector3 offset = Vector3.zero;

    [HideInInspector]
    public Transform targetTr;

    private const float viewdistance = 50.0f;
    public float dist;

    public Vector3 screenpos;
    public Vector2 localPos;
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
        screenpos = UnityServiceManager.WorldSpaceToScreenSpace(targetTr.position + offset);

        if (screenpos.z < 0.0f) screenpos *= -1.0f;

        localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenpos, uiCamera, out localPos);
        rectHp.localPosition = localPos;
        healthVisible();
    }

    public void healthVisible()
    {
        mainCameraPosition = UnityServiceManager.GetMainCameraPosition;
        dist = Vector3.Distance(targetTr.position, mainCameraPosition);
        if (dist > viewdistance)
            rectHp.transform.localScale = Vector3.zero;
        else
            rectHp.transform.localScale = Vector3.one;
    }

    public float getViewDistance()
    {
        return viewdistance;
    }

}
