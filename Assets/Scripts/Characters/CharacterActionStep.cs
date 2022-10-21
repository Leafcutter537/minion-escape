using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CharacterActionStep
{

    [Header("Title")]
    public string title;

    [Header("Animation")]
    public string animationVariable;
    public AnimationSetType animationSetType;

    [Header("Movement")]
    public float impulseStrength;

    [Header("Next Step")]
    public NextStep nextStep;
    public float duration;

    public enum AnimationSetType
    { 
        Trigger,
        SetBoolFalse,
        SetBoolTrue,
        None
    }

    public enum NextStep
    {
        AwaitInput,
        AnimationEvent,
        FixedDuration
    }


}
