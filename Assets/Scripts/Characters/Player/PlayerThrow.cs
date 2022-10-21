using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterAnimator characterAnimator;
    [SerializeField] private CharacterAction throwAction;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Animator noPotionWarning;
    [Header("Prefabs")]
    [SerializeField] private GameObject trajectoryPrefab;
    [SerializeField] private List<GameObject> projectilePrefabs;
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;



    private Vector3 projectileSpawnPosition { 
        get
        {
            Vector3 basePosition = projectileSpawnPoint.position;
            if (spriteRenderer.flipX)
                basePosition.x = transform.position.x  - (basePosition.x - transform.position.x);
            return basePosition;
        } 
    }
    public PlayerInputActions playerControls;
    private InputAction fire;
    private InputAction cancelFire;
    private bool isAiming;
    private TrajectoryIndicator indicator;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void Update()
    {
        if (isAiming)
        {
            FacePointer();
            SetTrajectoryVelocity();
                
        }
    }



    private void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        cancelFire = playerControls.Player.CancelFire;
        cancelFire.Enable();

        fire.performed += StartThrow;
        fire.canceled += FinishThrow;

        cancelFire.performed += CancelThrow;
    }

    private void OnDisable()
    {
        fire.Disable();
        cancelFire.Disable();
    }

    private void SetTrajectoryVelocity()
    {
        indicator.transform.position = projectileSpawnPosition;
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector2 spawnPosition = new Vector2(projectileSpawnPosition.x, projectileSpawnPosition.y);
        indicator.startingVelocity = (worldPosition - spawnPosition).normalized * projectileSpeed;
        indicator.SetDotPosition();
    }
    
    private void FacePointer()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        spriteRenderer.flipX = worldPosition.x < transform.position.x;
    }

    private void CreateProjectile()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 spawnPosition = projectileSpawnPosition;
        GameObject projectilePrefab = projectilePrefabs[(int)inventory.selectedType];
        GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectileObject.GetComponent<Rigidbody2D>().velocity = ((new Vector3(worldPosition.x, worldPosition.y, 0) - spawnPosition).normalized * projectileSpeed);
    }

    private void StartThrow(InputAction.CallbackContext context)
    {
        if (playerMovement.lastGroundedTime > 0 & !playerMovement.IsActing())
        {
            if (inventory.HasPotion())
            {
                throwAction.StartAction();
                indicator = Instantiate(trajectoryPrefab, projectileSpawnPosition, Quaternion.identity).GetComponent<TrajectoryIndicator>();
                indicator.gravity = Physics2D.gravity.y;
                indicator.CreateDots();
                SetTrajectoryVelocity();
                isAiming = true;
            }
            else
            {
                noPotionWarning.SetTrigger("NoPotions");
            }
        }
    }

    private void CancelThrow(InputAction.CallbackContext context)
    {
        if (isAiming == true)
        {
            throwAction.CancelAction();
            Destroy(indicator.gameObject);
            isAiming = false;
        }
    }

    private void FinishThrow(InputAction.CallbackContext context)
    {
        if (isAiming == true)
        {
            CreateProjectile();
            throwAction.NextStepInput();
            Destroy(indicator.gameObject);
            isAiming = false;
            inventory.UsePotion();
        }
    }


}
