using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meltable : MonoBehaviour
{
    [SerializeField] private CharacterAction meltAction;
   
    public void Melt()
    {
        meltAction.StartAction();
    }

}
