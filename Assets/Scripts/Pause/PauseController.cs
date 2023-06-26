using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [Header("Event References")]
    [SerializeField] private GamePauseEvent pauseEvent;
    [SerializeField] private GameUnpauseEvent unpauseEvent;
    [Header("Pausing")]
    public PlayerInputActions playerControls;
    private InputAction pause;
    private bool isPaused;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        pause = playerControls.PauseMenu.Pause;
        pause.Enable();
        pause.performed += OnPressPause;
    }
    private void OnDisable()
    {
        pause.Disable();
    }

    private void OnPressPause(InputAction.CallbackContext context)
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            unpauseEvent.Raise(this, null);
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            pauseEvent.Raise(this, null);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
