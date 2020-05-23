using UnityEditor;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{

    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public int Score { get; set; }
    [field: SerializeField] public int Money { get; set; }
    [field: SerializeField] public float Health { get; set; }

    public Inventory PlayerInventory = new Inventory();

    public PlayerStateController player;

    private Transform transform;

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
           PlayerInventory.Add(new Item(guns[i].GetComponent<ImWeapon>().EntityID,i,100,1));
        }
        equipped = PlayerInventory.Primary.Id;
    }

    void Update()
    {


       if(equipped != PlayerInventory.selected.Id)
        {
            player.UnEquipWeapon();
            for (int i = 0; i < guns.Length; i++)
            {
                if (PlayerInventory.selected.Id == guns[i].GetComponent<ImWeapon>().EntityID)
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
        Debug.Log("Button save going through");
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

        Debug.Log($"Player was LOADED. Health:{this.Health} Level:{this.Level} Money:{this.Money} Score:{this.Score}" +
            $"Position: x={transform.position[0]} y={transform.position[1]} y={transform.position[2]}");
    }
}
