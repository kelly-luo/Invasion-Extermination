using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImItem 
{
    int EntityID { get; set; }
    int InstanceID { get; set; }
    int StackAmount { get; set; }
    int StackLimit { get; }
}
