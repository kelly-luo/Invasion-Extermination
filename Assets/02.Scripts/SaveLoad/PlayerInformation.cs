using UnityEngine;

public class PlayerInformation : MonoBehaviour
{

    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public int Score { get; set; }
    [field: SerializeField] public int Money { get; set; }
    [field: SerializeField] public float Health { get; set; }
     public Inventory PlayerInventory { get; set; }

    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        PlayerInventory = new Inventory();
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
