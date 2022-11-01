using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] private CharacterAction lookAction;
    [SerializeField] private CameraTarget cameraTarget;

    public PlayerInputActions playerControls;
    private InputAction look;
    private InputAction moveCamera;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        look = playerControls.Player.Look;
        look.Enable();
        moveCamera = playerControls.Player.MoveCamera;
        moveCamera.Enable();
       

        look.performed += Look;
        look.canceled += StopLook;

    }

    private void OnDisable()
    {
        look.Disable();
    }

    private void Update()
    {
        Vector2 lookVector = moveCamera.ReadValue<Vector2>();
        if (lookAction.isActing)
        {
            cameraTarget.lookDirection = lookVector;
        }
        else
        {
            cameraTarget.lookDirection = Vector2.zero;
        }
    }


    private void Look(InputAction.CallbackContext context)
    {
        lookAction.StartAction();
    }

    private void StopLook(InputAction.CallbackContext context)
    {
        lookAction.NextStepInput();
    }


}
