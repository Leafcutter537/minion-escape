using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [HideInInspector]
    public CharacterAction currentAction;

    public void SetGrounded(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void HitboxCheck()
    {
        currentAction.HitBoxCheck();
    }

    public void Cancel()
    {
        animator.SetTrigger("Cancel");
    }

    public void SetIsWalking(bool isWalking)
    {
        animator.SetInteger("AnimState", isWalking? 1 : 0);
    }

    public void SetBool(string variableName, bool boolValue)
    {
        animator.SetBool(variableName, boolValue);
    }

    public void SetInt(string variableName, int intValue)
    {
        animator.SetInteger(variableName, intValue);
    }

    public void SetTrigger(string variableName)
    {
        animator.SetTrigger(variableName);
    }

    public void NextStepAnimation()
    {
        currentAction.NextStepAnimation();
    }
    

}
