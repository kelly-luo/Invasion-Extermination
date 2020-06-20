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
        Debug.Log($"PLayer hp: {Player.GetComponent<PlayerInformation>().Health}");
        Player.GetComponent<PlayerInformation>().Health += HealthAmount;
        Debug.Log($"PLayer GAINED hp: {Player.GetComponent<PlayerInformation>().Health}");
    }

}
