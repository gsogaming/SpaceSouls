using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossChallangeArea : MonoBehaviour
{

    public List<GameObject> bosses;

    [Tooltip("Challenge Door")]
    public GameObject challengeAreaDoor;

    [Tooltip("Number of Enemies to defeat before the door is opened")]
    public float numberOfEnemiesToDefeat;

    [Tooltip("Number of Enemies defeated")]
    public float enemiesDefeated;

    [Tooltip("Challenge completed or not")]
    public bool challengeIsCompleted;

    [Tooltip("Text for the challenge warning")]
    public TextMeshProUGUI warningText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !challengeIsCompleted)
        {
            // Player has entered the challenge area
            ActivateObjects();
            warningText.gameObject.SetActive(false);
            warningText.gameObject.SetActive(true);
            warningText.text = "Challenge : Kill " + numberOfEnemiesToDefeat + " bosses to leave the room";
        }       

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boss") && !challengeIsCompleted)
        {
            enemiesDefeated++;

            if (enemiesDefeated >= numberOfEnemiesToDefeat)
            {
                ChallengeIsCompleted();
            }
        }

       
    }

    private void ChallengeIsCompleted()
    {
        challengeIsCompleted = true;
        DeActivateObjects();
        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.text = "Challenge is completed!";
    }

    private void ActivateObjects()
    {
        // Activate the spawners
        foreach (GameObject boss in bosses)
        {
            boss.SetActive(true);
        }

        // Activate the challenge area door
        challengeAreaDoor.SetActive(true);
    }

    private void DeActivateObjects()
    {
        // Activate the spawners
        foreach (GameObject boss in bosses)
        {
            boss.SetActive(false);
        }

        // Activate the challenge area door
        challengeAreaDoor.SetActive(false);
    }





}
