using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AggroBehavior : BehaviorPattern
{
    public LayerMask aggroTargets;

    public abstract void Aggro(Transform target);
    public abstract void Deaggro(Transform target);

}
