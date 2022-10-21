using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAction : MonoBehaviour
{

    [Header("Title")]
    public string title;

    [Header("References")]
    [SerializeField] private CharacterAnimator characterAnimator;
    [SerializeField] protected new Rigidbody2D rigidbody;
    [SerializeField] private CharacterMovement characterMovement;

    [Header("Action Steps")]
    public List<CharacterActionStep> actionSteps;

    [Header("Hitbox")]
    public Transform[] hitPoints;
    public float hitBoxSize;
    [SerializeField] private new Collider2D collider;


    private int currentStep;
    [HideInInspector]
    public bool isActing;




    public virtual void StartAction()
    {
        currentStep = 0;
        isActing = true;
        characterAnimator.currentAction = this;
        PerformStep();
    }

    private void PerformStep()
    {

        AnimationStep();
        MovementStep();
    }


    private void AnimationStep()
    {
        string animationVariable = actionSteps[currentStep].animationVariable;
        switch (actionSteps[currentStep].animationSetType)
        {
            case (CharacterActionStep.AnimationSetType.SetBoolFalse):
                characterAnimator.SetBool(animationVariable, false);
                break;
            case (CharacterActionStep.AnimationSetType.SetBoolTrue):
                characterAnimator.SetBool(animationVariable, true);
                break;
            case (CharacterActionStep.AnimationSetType.Trigger):
                characterAnimator.SetTrigger(animationVariable);
                break;
        }
    }

    private void MovementStep()
    {
        float impulseStrength = actionSteps[currentStep].impulseStrength;
        if (impulseStrength != 0)
        {
            Vector2 direction = characterMovement.spriteRenderer.flipX ? Vector2.left : Vector2.right;
            rigidbody.AddForce(direction * impulseStrength, ForceMode2D.Impulse);
        }
    }

    public void HitBoxCheck()
    {
        if (hitPoints.Length != 0)
        {
            for (int i = 0; i < hitPoints.Length; i++)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(GetHitPointPosition(i), hitBoxSize);
                foreach (Collider2D collider in colliders)
                {
                    if (collider != this.collider)
                    {
                        CharacterHazard characterHazard = collider.GetComponent<CharacterHazard>();
                        if (characterHazard != null)
                        {
                            if ((characterHazard.hazardLayers.value & 1 << gameObject.layer) == 1 << gameObject.layer)
                            {
                                characterHazard.Die();
                            }
                        }
                    }
                }
            }
        }
    }

    private Vector3 GetHitPointPosition(int index)
{
        Vector3 basePosition = hitPoints[index].position;
        if (characterMovement.spriteRenderer.flipX)
            basePosition.x = transform.position.x - (basePosition.x - transform.position.x);
        return basePosition;
       
    }

    private void NextStep()
    {
        currentStep++;
        if (currentStep == actionSteps.Count)
        {
            EndAction();
        }
        else
        {
            PerformStep();
        }
    }


    public void NextStepAnimation()
    {
        if (actionSteps[currentStep].nextStep == CharacterActionStep.NextStep.AnimationEvent)
        {
            NextStep();
        }
    }

    public bool NextStepInput()
    {
        if (actionSteps[currentStep].nextStep == CharacterActionStep.NextStep.AwaitInput)
        {
            NextStep();
            return true;
        }
        return false;
    }

    public void CancelAction()
    {
        isActing = false;
        characterAnimator.Cancel();
    }

    protected virtual void EndAction()
    {
        isActing = false;
    }



}
