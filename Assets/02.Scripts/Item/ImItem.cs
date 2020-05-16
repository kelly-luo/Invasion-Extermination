using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ImItem 
{
    int EntityID { get; }

    int InstanceID { get; }

    int LimitStacking { get; }
}

