using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImItem 
{
    int EntityID { get; set; }

    int InstanceID { get; }

    int limitStacking { get; }
}

