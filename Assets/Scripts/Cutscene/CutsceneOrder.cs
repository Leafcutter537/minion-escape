using System;
using UnityEngine;

[Serializable]
public class CutsceneOrder
{

    public Transform orderRecipient;
    public Transform moveLocation;
    public bool faceLeft;
    public OrderType orderType;
    public string animationName;


    public enum OrderType
    { 
        StartSpawning,
        MoveToLocation,
        Animation,
        Teleport
    }

    public void PerformOrder()
    {
        switch (orderType)
        {
            case OrderType.MoveToLocation:
                MoveToPoint moveToPoint = orderRecipient.GetComponent<MoveToPoint>();
                moveToPoint.isActive = true;
                if (moveLocation != moveToPoint.guardPoint)
                {
                    moveToPoint.guardPoint = moveLocation;
                    moveToPoint.isInGuardPoint = false;
                }
                moveToPoint.faceLeftDefault = faceLeft;
                break;
            case OrderType.Animation:
                CharacterAnimator characterAnimator = orderRecipient.GetComponent<CharacterMovement>().characterAnimator;
                characterAnimator.SetTrigger(animationName);
                break;
            case OrderType.StartSpawning:
                CutsceneSpawner cutsceneSpawner = orderRecipient.GetComponent<CutsceneSpawner>();
                cutsceneSpawner.Activate();
                break;
            case OrderType.Teleport:
                orderRecipient.transform.position = moveLocation.position;
                break;
        }
    }



}
