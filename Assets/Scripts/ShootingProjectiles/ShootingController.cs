using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// A class which controlls player aiming and shooting
/// </summary>
public class ShootingController : MonoBehaviour
{
    [Header("GameObject/Component References")]
    [Tooltip("The projectile to be fired.")]
    public GameObject projectilePrefab = null;
    [Tooltip("The transform in the heirarchy which holds projectiles if any")]
    public Transform projectileHolder = null; 


    [Header("Input")]
    [Tooltip("Whether this shooting controller is controled by the player")]
    public bool isPlayerControlled = false;

    [Header("Firing Settings")]
    [Tooltip("The minimum time between projectiles being fired.")]
    public float fireRate = 0.05f;

    [Tooltip("The maximum diference between the direction the" +
        " shooting controller is facing and the direction projectiles are launched.")]
    public float projectileSpread = 1.0f;

    [Header("Overheat")]
    [Tooltip("The maximum heat value before overheating.")]
    public float maxHeat = 100.0f;

    [Tooltip("The rate at which heat dissipates per second when not firing.")]
    public float heatDissipationRate = 20.0f;
    [Tooltip("The rate at which the weapon heats up")]
    public float heatRate = 10f;
    public float currentHeat = 0.0f;

    [Tooltip("The cooldown time in seconds after the weapon overheats.")]
    public float cooldownTime = 2.0f;

    public bool isCoolingDown = false;
    public float cooldownTimer = 0.0f;

    [Header("Player Overheat Slider Settings")]
    public Slider overHeatBarSlider;
    public Animator overHeatBarAnim;
    

    // The last time this component was fired
    private float lastFired = Mathf.NegativeInfinity;

    [Header("Effects")]
    [Tooltip("The effect to create when this fires")]
    public GameObject fireEffect;

    //The input manager which manages player input
    private InputManager inputManager = null;

    /// <summary>
    /// Description:
    /// Standard unity function that runs every frame
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Update()
    {
        ProcessInput();

        if (overHeatBarSlider != null)
        {
            overHeatBarSlider.value = currentHeat/maxHeat;
        }

        if (isCoolingDown)
        {
            // Start the cooldown timer
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0.0f)
            {
                // Cooldown finished, allow firing again
                isCoolingDown = false;

                //Overheat bar blinks
                if (overHeatBarAnim != null)
                {
                    overHeatBarAnim.SetInteger("AnimState", 0);
                }
                

                // Reset current heat after cooldown
                currentHeat = Mathf.Lerp(currentHeat, 0.0f,1);
            }
        }
        else if (isPlayerControlled && currentHeat > 0.0f)
        {
            // Dissipate heat when the weapon is not firing
            currentHeat -= heatDissipationRate * Time.deltaTime;
            currentHeat = Mathf.Clamp(currentHeat, 0.0f, maxHeat);

            // Check if the heat has dropped below a threshold
            if (currentHeat <= maxHeat * 0.5f)
            {
                // Enable firing when heat is low enough
                isCoolingDown = false;
            }
            
        }
    }

    /// <summary>
    /// Description:
    /// Standard unity function that runs when the script starts
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Start()
    {
        SetupInput();

        
    }

    /// <summary>
    /// Description:
    /// Attempts to set up input if this script is player controlled and input is not already correctly set up 
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    void SetupInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager == null)
            {
                inputManager = InputManager.instance;
            }
            if (inputManager == null)
            {
                Debug.LogError("Player Shooting Controller can not find an InputManager in the scene, there needs to be one in the " +
                    "scene for it to run");
            }
        }
    }

    /// <summary>
    /// Description:
    /// Reads input from the input manager
    /// Inputs:
    /// None
    /// Returns:
    /// void (no return)
    /// </summary>
    void ProcessInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager.firePressed || inputManager.fireHeld)
            {
                Fire();
            }
        }   
    }

    /// <summary>
    /// Description:
    /// Fires a projectile if possible
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void Fire()
    {
        // Check if the weapon is not overheated
        if (currentHeat < maxHeat && !isCoolingDown)
        {
            // If the cooldown is over, fire a projectile
            if ((Time.timeSinceLevelLoad - lastFired) > fireRate)
            {
                // Launch a projectile
                SpawnProjectile();

                if (fireEffect != null)
                {
                    Instantiate(fireEffect, transform.position, transform.rotation, null);
                }

                // Increase the current heat
                currentHeat += fireRate * heatRate;

                // Clamp the heat value to not exceed the maximum
                currentHeat = Mathf.Clamp(currentHeat, 0.0f, maxHeat);

                // Restart the cooldown
                lastFired = Time.timeSinceLevelLoad;

                if (currentHeat >= maxHeat)
                {
                    // Start the cooldown timer
                    cooldownTimer = cooldownTime;
                    isCoolingDown = true;
                    if (overHeatBarAnim != null)
                    {
                        overHeatBarAnim.SetInteger("AnimState", 1);
                    }
                    
                }

            }
        }
    }

    /// <summary>
    /// Description:
    /// Spawns a projectile and sets it up
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void SpawnProjectile()
    {
        // Check that the prefab is valid
        if (projectilePrefab != null)
        {
            // Create the projectile            
            GameObject projectileGameObject = Instantiate(projectilePrefab, transform.position, transform.rotation, null);

            // Account for spread
            Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
            rotationEulerAngles.z += Random.Range(-projectileSpread, projectileSpread);
            projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);

            // Keep the heirarchy organized
            if (projectileHolder != null)
            {
                projectileGameObject.transform.SetParent(projectileHolder);
            }
        }
    }

    // Call this method to start the heat reset coroutine
    
}
