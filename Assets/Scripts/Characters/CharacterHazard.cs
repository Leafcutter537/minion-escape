using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHazard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterAnimator characterAnimator;
    [SerializeField] private CharacterAction deathAction;
    [Header("Hazards")]
    public LayerMask hazardLayers;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((hazardLayers.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!deathAction.isActing)
            deathAction.StartAction();
    }
}
