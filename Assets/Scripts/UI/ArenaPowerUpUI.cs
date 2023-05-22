using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaPowerUpUI : MonoBehaviour
{
    [Tooltip("Player's ship")]
    public GameObject player;

    [Tooltip("PowerUp UI")]
    public GameObject powerUpUI;


    [Tooltip("Powerup effects")]
    public int healthIncreaseAmount = 3;
    public float fireRateDecreaseAmount = 1.3f;

    // Method to be called when the health powerup button is clicked
    public void ApplyHealthPowerup()
    {
        // Check if the player has the Health script attached
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Increase the player's health
            playerHealth.ReceiveHealing(healthIncreaseAmount);
        }

        powerUpUI.SetActive(false);
        Time.timeScale = 1;
    }

    // Method to be called when the fire rate powerup button is clicked
    public void ApplyFireRatePowerup()
    {
        // Check if the player has the ShootController script attached
        ShootingController shootingController = player.GetComponent<ShootingController>();
        if (shootingController != null)
        {
            // Decrease the player's fire rate
            shootingController.fireRate *= fireRateDecreaseAmount;
        }

        powerUpUI.SetActive(false);
        Time.timeScale = 1;
    }



}
