using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CutsceneStep
{

    [Header("Title")]
    public string title;


    [Header("Character Orders")]
    public CutsceneOrder[] orders;

    [Header("Dialogue")]
    public string dialogue;



    [Header("Next Step")]
    public NextStep nextStep;
    public float duration;
    public NonplayerMovement character;
    public Transform point;



    public void PerformStep()
    {
        foreach (CutsceneOrder order in orders)
        {
            order.PerformOrder();
        }
    }

    public void SendDialogue(DialoguePanel dialoguePanel, PortraitPairings portraitPairings)
    {
        string[] lines = dialogue.Split("$");
        string[] text = new string[lines.Length];
        Sprite[] portraits = new Sprite[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] splitLine = lines[i].Split(":");
            portraits[i] = portraitPairings.GetPortrait(splitLine[0].Trim());
            text[i] = splitLine[1].Trim();
        }
        dialoguePanel.SetDialogue(text, portraits);
    }



    public enum NextStep
    {
        AwaitDialogue,
        CharacterReachesPoint,
        FixedDuration
    }


}
