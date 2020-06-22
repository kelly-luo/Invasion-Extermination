﻿//
// PlayerInformation INFORMATION FOR PLAYER
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// This class manages all the information about the player such as stats.
// This includes player score, money, health, inventory, position, guns.
// 
// AUT University - 2020 - Kelly Luo, Yuki Liyanage, Dan Yoo, Howard Mao
// 
// Revision History
// ~~~~~~~~~~~~~~~~
// 18.05.2020 Creation date (Kelly)
// 23.05.2020 Added player inventory to equip/unequip guns (Yuki)

//
// Unity support packages
// ~~~~~~~~~~~~~~~~~~~~~~
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    private const float MAX_HEALTH = 100f;

    private int score = 0;
    [field: SerializeField] public int Score
    {
        get { return score; }
        set { if ((value) <= 0) { score = 0; } else score = value; }
    }
    public int money = 0;
    [field: SerializeField] public int Money {
        get { return money; }
        set { money = value; } 
    }


    private float health = 100f;
    [field: SerializeField] public float Health 
    { 
        get { return health; }
        set { if (value <= 0)
            { 
                health = 0; 
            }
            else if (value >= MAX_HEALTH)
            {
                health = MAX_HEALTH;
            }
            else health = value; 
        
        }

    }

    public PlayerStateController player;
    public Transform transform;
    public Inventory PlayerInventory = new Inventory();
    public GameObject[] guns;
    public int equipped;
    public int ammo  = 100;

    [field: SerializeField] public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }
    }


    //
    // Start()
    // ~~~~~~~~~~~~
    // Upon start of game, player transform in instantiated from player object and player equips primary gun.
    //
    void Start()
    {
        transform = GetComponent<Transform>();
       
        PlayerStateController controller = GetComponent<PlayerStateController>();
        GameObject weapon = controller.weapon;
        PlayerInventory.Add(weapon.GetComponent<ImWeapon>());

        player.IsHoldingRifle = true;
        equipped = weapon.GetComponent<ImWeapon>().EntityID;
    }

    //
    // Update()
    // ~~~~~~~~~~~~
    // Upon every frame, checks if the player has chosen to equip another gun from the inventory and
    // if so then it changes the equiped gun.
    //
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

    //
    // SavePlayer()
    // ~~~~~~~~~~~~
    // This method parses the current player information instance into the SaveSystem to be binary serialised to a save file.
    //
    // returns      True if the player information was successfully saved in the SaveSystem without disruption
    //
    public bool SavePlayer()
    {
        return SaveSystem.SavePlayer(this);
    }

    //
    // LoadPlayer()
    // ~~~~~~~~~~~~
    // Loads the save data from a local save file and updating the position, health, score and money to the player.
    //
    // returns      True if the player information was successfully loaded in the SaveSystem and was not null
    //
    public bool LoadPlayer()
    {
        PlayerSaveData loadedData = SaveSystem.LoadPlayer();

        if (loadedData == null) return false;

        Vector3 position;
        position.x = loadedData.position[0];
        position.y = loadedData.position[1];
        position.z = loadedData.position[2];

        this.transform.position = position;
        this.Health = loadedData.Health;
        this.Score = loadedData.Score;
        this.Money = loadedData.Money;

        Debug.Log($"Player was LOADED. Health:{loadedData.Health} Money:{loadedData.Money} " +
        $"Score:{loadedData.Score} Position: x={loadedData.position[0]} y={loadedData.position[1]} y={loadedData.position[2]}");

        return true;

    }
}
