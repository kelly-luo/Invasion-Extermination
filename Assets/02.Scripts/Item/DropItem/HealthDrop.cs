//
// HealthDrop HEALTH ITEM DROP FROM ENEMIES AND HUMANS
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// This class controls the health drop prefab by enabling, disabling, collision with player as well as spawn pop up effect.
// This is done by keeping track of the transform and rigidbody of the health drop prefab object.
// 
// AUT University - 2020 - Kelly Luo
// 
// Revision History
// ~~~~~~~~~~~~~~~~
// 20.05.2020 Creation date

//
// Unity support packages
// ~~~~~~~~~~~~~~~~~~~~~
using UnityEngine;

public class HealthDrop : MonoBehaviour, ImDropableItem
{

    public Transform ObjectTransform { get; set; }

    public Vector3 InitialScale { get; set; } = new Vector3(1f, 1f, 1f);

    private Rigidbody rigidbody;

    public float HealthAmount { get; set; } = 30f;

    public int PoppingSpeed { get; set; } = 1000;
    public int PopingRandomRangeRadius { get; set; } = 3;

    void Awake()
    {
        ObjectTransform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();

    }

    void OnEnable()
    {
        gameObject.transform.localPosition = ObjectTransform.position;
        gameObject.transform.localScale = InitialScale;
        gameObject.transform.rotation = Quaternion.Euler(270, 90, 0);
        InitialSpawnLootPopUpEffect();
    }

    void OnDisable()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    //
    // OnCollisionEnter()
    // ~~~~~~~~~~~~~~~~~~
    // It is checked if the Player was the one that collided (walked on) with the health drop prefab.
    // If the player is the one that collided with the health drop prefab then it will disable active state of prefab object.
    //
    // coll   Incoming object that has collided with the prefab
    //
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            OnCollisionWithPlayer(coll.gameObject);

            gameObject.SetActive(false);
        }
    }

    //
    // InitialSpawnLootPopUpEffect()
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // This method adds force to the rigidbody and allowing to pop up when spawned
    //
    public void InitialSpawnLootPopUpEffect()
    {
        var yAmountOfForceVector = new Vector3(0f, 1f, 0f);
        var zAmountOfForceVector = new Vector3(0f, 0f, UnityServiceManager.Instance.UnityRandomRange(-PopingRandomRangeRadius, PopingRandomRangeRadius) * 0.1f);
        var xAmountOfForceVector = new Vector3(UnityServiceManager.Instance.UnityRandomRange(-PopingRandomRangeRadius, PopingRandomRangeRadius) * 0.1f, 0f, 0f);

        var totalUnitVector = (yAmountOfForceVector + zAmountOfForceVector + xAmountOfForceVector).normalized;

        rigidbody.AddForce(totalUnitVector * 15);
    }

    //
    // OnCollisionAddHealth()
    // ~~~~~~~~~~~~~~~~~~~~~~
    // On collision with player, add health to the player stats
    //
    // player   player object
    //
    public void OnCollisionWithPlayer(GameObject Player)
    {
        Debug.Log($"PLayer hp: {Player.GetComponent<PlayerInformation>().Health}");
        Player.GetComponent<PlayerInformation>().Health += HealthAmount;
        Debug.Log($"PLayer GAINED hp: {Player.GetComponent<PlayerInformation>().Health}");
    }

}
