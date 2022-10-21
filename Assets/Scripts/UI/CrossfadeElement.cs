using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadeElement : MonoBehaviour
{
    [HideInInspector]
    public bool isFadingIn;
    [HideInInspector]
    public bool isFadingOut;
    [SerializeField] private Animator animator;

    [SerializeField] private LevelLoadCrossfade crossfadeController;


    public void FadeIn()
    {
        isFadingIn = true;
        animator.SetTrigger("FadeIn");
    }

    public void NextFadeIn()
    {
        crossfadeController.NextFadeIn();
    }

    public void FadeOut()
    {
        isFadingOut = true;
        animator.SetTrigger("FadeOut");
    }

    public void NextFadeOut()
    {
        crossfadeController.NextFadeOut();
    }

}
