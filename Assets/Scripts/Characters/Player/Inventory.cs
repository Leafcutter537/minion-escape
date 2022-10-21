using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public Dictionary<PotionPickup.PotionType, int> potions;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private CharacterAnimator characterAnimator;

    public PotionPickup.PotionType selectedType;
    public PlayerInputActions playerControls;
    private InputAction selectOne;
    private InputAction selectTwo;
    private InputAction selectThree;

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
        selectOne.Enable();
        selectTwo.Enable();
        selectThree.Enable();

        selectOne.performed += SelectPotionOne;
        selectTwo.performed += SelectPotionTwo;
        selectThree.performed += SelectPotionThree;
    }

    private void OnDisable()
    {
        selectOne.Disable();
        selectTwo.Disable();
        selectThree.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PotionPickup")
        {
            PotionPickup potionPickup = collision.GetComponent<PotionPickup>();
            potions[potionPickup.potionType] += 1;
            inventoryUI.UpdatePotionUI();
            potionPickup.PickUpAnimation();
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

    private void SelectPotionOne(InputAction.CallbackContext context)
    {
        selectedType = (PotionPickup.PotionType) 0;
        characterAnimator.SetInt("PotionType", 0);
        inventoryUI.UpdatePotionUI();
    }

    private void SelectPotionTwo(InputAction.CallbackContext context)
    {
        selectedType = (PotionPickup.PotionType) 1;
        characterAnimator.SetInt("PotionType", 1);
        inventoryUI.UpdatePotionUI();
    }   
    
    private void SelectPotionThree(InputAction.CallbackContext context)
    {
        selectedType = (PotionPickup.PotionType) 2;
        characterAnimator.SetInt("PotionType", 2);
        inventoryUI.UpdatePotionUI();
    }
}
