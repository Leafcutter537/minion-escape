using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private DialoguePanel dialoguePanel;
    [SerializeField] private PortraitPairings portraitPairings;
    [Header("Events")]
    [SerializeField] private NextLevelLoadEvent nextLevelLoadEvent;
    [Header("Cutscene Steps")]
    public CutsceneStep[] steps;
    private int currentStep;
    private float timeToNextStep;


    private void Awake()
    {
        PerformStep();
    }

    private void Update()
    {
        if (timeToNextStep > 0)
        {
            timeToNextStep -= Time.deltaTime;
            if (timeToNextStep <= 0)
            {
                NextStep();
            }
        }
    }

    private void NextStep()
    {
        currentStep++;
        if (currentStep >= steps.Length)
        {
            nextLevelLoadEvent.Raise(this, null);
        }
        else
        {
            PerformStep();
        }
    }

    private void PerformStep()
    {
        bool dialoguePresent = steps[currentStep].dialogue.Length > 0;
        dialoguePanel.gameObject.SetActive(dialoguePresent);
        if (dialoguePresent)
        {
            steps[currentStep].SendDialogue(dialoguePanel, portraitPairings);
        }
        steps[currentStep].PerformStep();
        if (steps[currentStep].nextStep == CutsceneStep.NextStep.FixedDuration)
        {
            timeToNextStep = steps[currentStep].duration;
        }
    }

    public void CharacterArrived(NonplayerMovement character, Transform point)
    {
        if (currentStep >= steps.Length)
            return;
        if (steps[currentStep].nextStep == CutsceneStep.NextStep.CharacterReachesPoint)
        {
            if (character == steps[currentStep].character & point == steps[currentStep].point)
                NextStep();
        }
    }

    public void DialogueFinished()
    {
        if (steps[currentStep].nextStep == CutsceneStep.NextStep.AwaitDialogue)
        {
            NextStep();
        }
    }
}
