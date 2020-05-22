using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Id { get; set; }
    public int InstanceId { get; set; }
    public int Damage { get; }
    public int Amount { get; set; }
    public Item(int id, int InstanceId, int damage, int stackLimit)
    {
        this.Id = id;
        this.InstanceId = InstanceId;
        this.Damage = damage;
        this.Amount = stackLimit;

    }

}
