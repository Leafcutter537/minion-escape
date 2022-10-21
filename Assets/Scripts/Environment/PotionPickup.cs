using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup : MonoBehaviour
{

    public PotionType potionType;
    [SerializeField] private ParticleSystem acquireParticles;
    [SerializeField] private Animator animator;

    public void PickUpAnimation()
    {
        acquireParticles.Play();
        animator.SetTrigger("Acquire");
    }





    public enum PotionType
    {
        Acidic,
        Rage,
        Mushroom
    }


}
