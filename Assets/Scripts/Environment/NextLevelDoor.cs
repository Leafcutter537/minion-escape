using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{


    [SerializeField] private Animator animator;
    [SerializeField] private NextLevelLoadEvent nextLevelLoadEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerEndLevelAction endLevelAction = collision.GetComponent<PlayerEndLevelAction>();
            endLevelAction.StartAction();
            Destroy(collision.GetComponent<CharacterHazard>());
            animator.SetTrigger("Open");
            nextLevelLoadEvent.Raise(this, null);
        }
    }
}
