using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float fireRateIncrement;
    public int hpIncrease;

    private void OnTriggerEnter2D(Collider2D other)
    {      

        if (other.CompareTag("Player") && gameObject.tag == "FireRatePowerUp")
        {           

            other.GetComponent<ShootingController>().fireRate *= fireRateIncrement; // Adjust this value to control the increment
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && gameObject.tag == "HpPowerUp")
        {
            other.GetComponent<Health>().ReceiveHealing(hpIncrease);
            Destroy(gameObject);
        }
    }
}
