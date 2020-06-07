using UnityEditor;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{

    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public int Score { get; set; }
    [field: SerializeField] public int Money { get; set; }
    [field: SerializeField] public int Ammo { get; set; }

    [field: SerializeField] public float health = 100f;

    [field: SerializeField] public float Health 
    { 
        get { return health; }
        set { if (health <= 0) { health = 0; } else health = value; } // Do not allow health to go below 0
    }

    public Inventory PlayerInventory = new Inventory();

    public PlayerStateController player;

    public Transform transform;

    public GameObject[] guns;

    public int equipped;

    void Start()
    {
        transform = GetComponent<Transform>();
        if (player.HasWeapon)
        {
            player.UnEquipWeapon();
        }

        for (int i = 0; i < guns.Length; i++)
        {
            var gun = guns[i].GetComponent<ImWeapon>();
            gun.InstanceID = i;
            gun.StackAmount = 1;

            PlayerInventory.Add(gun);
        }
        equipped = PlayerInventory.Primary.EntityID;
    }

    void Update()
    {


        if(equipped != PlayerInventory.selected.EntityID)
        {
            player.UnEquipWeapon();
            for (int i = 0; i < guns.Length; i++)
            {
                if (PlayerInventory.selected.EntityID == guns[i].GetComponent<ImWeapon>().EntityID)
                {
                    player.EquipWeapon(guns[i]);
                    player.IsHoldingRifle = true;
                    equipped = guns[i].GetComponent<ImWeapon>().EntityID;
                }
            }
        }
    }


    public void SavePlayer()
    {

        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerStats data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
        this.Health = data.Health;
        this.Score = data.Score;
        this.Level = data.Level;
        this.Score = data.Score;


    }
}
