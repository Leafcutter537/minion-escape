using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyEnrage : MonoBehaviour
{
    [SerializeField] private CharacterAction enrageAction;

    public void Enrage()
    {
        enrageAction.StartAction();
    }


}
