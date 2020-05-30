using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualItem :ImItem
{
    public int EntityID { get; set; }
    public int InstanceID { get; set; }
    public int Weight { get; }
    public int StackAmount { get; set; }
    public int StackLimit { get; } = 64;

    public VirtualItem(int entityID, int instanceID, int weight, int stackAmount)
    {
        this.EntityID = entityID;
        this.InstanceID = instanceID;
        this.Weight = weight;
        this.StackAmount = stackAmount;
    }

}
