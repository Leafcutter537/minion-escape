using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDestination : MonoBehaviour
{
    [SerializeField] private CutsceneController controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NonplayerMovement character = collision.GetComponent<NonplayerMovement>();
        if (character != null)
            controller.CharacterArrived(character, transform);
    }
}
