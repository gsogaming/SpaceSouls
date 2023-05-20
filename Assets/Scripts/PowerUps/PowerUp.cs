using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.CompareTag("Player") && gameObject.tag == "FireRatePowerUp")
        {
            float fireRateIncrement = 0.9f; // Adjust this value to control the increment

            other.GetComponent<ShootingController>().fireRate *= fireRateIncrement; // Adjust this value to control the increment
            Destroy(gameObject);
        }
    }
}
