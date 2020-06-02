using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, ImDropableItem
{

    public Transform ObjectTransform { get; set; }

    public Vector3 InitialScale { get; set; } = new Vector3(1f, 1f, 1f);

    private Rigidbody rigidbody;

    public int MoneyAmount { get; set; } = 10;

    public int PopingSpeed { get; set; } = 1000;
    public int PopingRandomRangeRadius { get; set; } = 3;
    
    void Awake()
    {
        ObjectTransform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();

    }

    void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(6f, 6f, 6f);
        InitialSpawnLootPopUpEffect();
    }

    void OnDisable()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            OnCollisionWithPlayer(coll.gameObject);

            gameObject.SetActive(false);
        }
    }

    public void InitialSpawnLootPopUpEffect()
    {
        var yAmountOfForceVector = new Vector3(0f, 1f, 0f);
        var zAmountOfForceVector = new Vector3(0f, 0f, UnityServiceManager.Instance.UnityRandomRange(-PopingRandomRangeRadius, PopingRandomRangeRadius) * 0.1f);
        var xAmountOfForceVector = new Vector3(UnityServiceManager.Instance.UnityRandomRange(-PopingRandomRangeRadius, PopingRandomRangeRadius) * 0.1f, 0f, 0f);

        var totalUnitVector = (yAmountOfForceVector + zAmountOfForceVector + xAmountOfForceVector).normalized;

        rigidbody.AddForce(totalUnitVector * 15);
    }

    public void OnCollisionWithPlayer(GameObject Player)
    {
        Player.GetComponent<PlayerInformation>().Money += MoneyAmount;
    }

}
