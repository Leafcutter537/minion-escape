using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Dictionary<PotionPickup.PotionType, int> potions;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private CharacterAnimator characterAnimator;
    [SerializeField] private bool disablePotionSelectOnStage;
    [HideInInspector]
    public bool isThrowingPotion;
    [HideInInspector]
    public PotionPickup.PotionType selectedType;
    [HideInInspector]
    public PlayerInputActions playerControls;
    private InputAction selectOne;
    private InputAction selectTwo;
    private InputAction selectThree;
    private InputAction cycleUp;
    private InputAction cycleDown;

    private void Awake()
    {
        potions = new Dictionary<PotionPickup.PotionType, int>();
        foreach (PotionPickup.PotionType type in Enum.GetValues(typeof(PotionPickup.PotionType)))
        {
            potions.Add(type, 0);
        }
        selectedType = 0;
        playerControls = new PlayerInputActions();
        inventoryUI.UpdatePotionUI();
    }

    private void OnEnable()
    {
        selectOne = playerControls.Player.SelectOne;
        selectTwo = playerControls.Player.SelectTwo;
        selectThree = playerControls.Player.SelectThree;
        cycleUp = playerControls.Player.CycleUp;
        cycleDown = playerControls.Player.CycleDown;
        selectOne.Enable();
        selectTwo.Enable();
        selectThree.Enable();
        cycleDown.Enable();
        cycleUp.Enable();

        selectOne.performed += SelectPotionOne;
        selectTwo.performed += SelectPotionTwo;
        selectThree.performed += SelectPotionThree;
        cycleDown.performed += CycleDown;
        cycleUp.performed += CycleUp;
    }

    private void OnDisable()
    {
        selectOne.Disable();
        selectTwo.Disable();
        selectThree.Disable();
        cycleUp.Disable();
        cycleDown.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PotionPickup")
        {
            PotionPickup potionPickup = collision.GetComponent<PotionPickup>();
            if (!potionPickup.pickedUp)
            {
                potionPickup.pickedUp = true;
                potions[potionPickup.potionType] += 1;
                inventoryUI.UpdatePotionUI();
                potionPickup.PickUpAnimation();
            }
        }
    }

    public bool HasPotion()
    {
        return potions[selectedType] > 0;
    }

    public void UsePotion()
    {
        potions[selectedType]--;
        inventoryUI.UpdatePotionUI();
    }

    private void SelectPotion(int potionIndex)
    {
        if (!disablePotionSelectOnStage & !isThrowingPotion)
        {
            selectedType = (PotionPickup.PotionType)potionIndex;
            characterAnimator.SetInt("PotionType", potionIndex);
            inventoryUI.UpdatePotionUI();
        }
    }

    private void SelectPotionOne(InputAction.CallbackContext context)
    {
        SelectPotion(0);
    }

    private void SelectPotionTwo(InputAction.CallbackContext context)
    {
        SelectPotion(1);
    }   
    
    private void SelectPotionThree(InputAction.CallbackContext context)
    {
        SelectPotion(2);
    }

    private void CycleUp(InputAction.CallbackContext context)
    {
        int potionIndex = (int) selectedType;
        potionIndex++;
        if (potionIndex == Enum.GetValues(typeof(PotionPickup.PotionType)).Length)
        {
            potionIndex = 0;
        }
        SelectPotion(potionIndex);
    }

    private void CycleDown(InputAction.CallbackContext context)
    {
        int potionIndex = (int)selectedType;
        potionIndex--;
        if (potionIndex == -1)
        {
            potionIndex = Enum.GetValues(typeof(PotionPickup.PotionType)).Length - 1;
        }
        SelectPotion(potionIndex);
    }
}
