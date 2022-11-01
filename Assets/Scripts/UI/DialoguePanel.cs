using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textObject;
    [SerializeField] private Image portraitImage;
    [Header("Text Appearance")]
    [SerializeField] private float textSpeed;
    private float timeSinceLastUpdate;
    #region Text Content
    public string[] dialogueText;
    private Sprite[] dialogueSpeakers;
    private int index;
    #endregion
    #region Controls
    public PlayerInputActions playerControls;
    private InputAction nextDialogue;
    #endregion

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        nextDialogue = playerControls.Cutscene.Continue;
        nextDialogue.Enable();
        nextDialogue.performed += NextDialogue;
    }

    private void OnDisable()
    {
        nextDialogue.Disable();
    }

    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        int numChars = Mathf.Min((int)(timeSinceLastUpdate * textSpeed), dialogueText[index].Length);
        string text = dialogueText[index];
        textObject.text = text.Substring(0, numChars) + "<color=#ff000000>" + text.Substring(numChars, text.Length - numChars) + "</color>";
    }

    private void NextDialogue(InputAction.CallbackContext context)
    {
        timeSinceLastUpdate = 0;
        index++;
        if (index < dialogueText.Length)
        {
            portraitImage.sprite = dialogueSpeakers[index];
        }
        else
        {
            index = 0;
            FindObjectOfType<CutsceneController>().DialogueFinished();
        }
    }

    public void SetDialogue(string[] text, Sprite[] portraits)
    {
        dialogueText = text;
        dialogueSpeakers = portraits;
        portraitImage.sprite = dialogueSpeakers[0];
    }

}
