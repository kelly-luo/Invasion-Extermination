using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImItem 
{
    int EntityID { get; }

    int InstanceID { get; set; }

    int LimitStacking { get; }
}

