using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorPattern : MonoBehaviour
{

    [SerializeField] protected NonplayerMovement enemyMovement;

    [HideInInspector]
    public bool isActive;
    public abstract void ActiveBehavior();
    public abstract void InactiveBehavior();

}
