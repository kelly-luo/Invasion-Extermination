
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This is Interface of basic Item every Item class should have this.
public interface ImItem 
{
    int EntityID { get; set; }
    int InstanceID { get; set; }
    int StackAmount { get; set; }
    int StackLimit { get; }

}
