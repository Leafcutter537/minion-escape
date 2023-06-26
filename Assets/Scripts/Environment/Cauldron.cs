using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public PotionPickup.PotionType potionType;
    public int potionCap;
    [SerializeField] private ParticleSystem acquireParticles;

    public void PickUpAnimation()
    {
        acquireParticles.Play();
    }
}
