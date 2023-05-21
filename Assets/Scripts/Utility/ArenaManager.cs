using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{

    public List<GameObject> spawners;

    [Tooltip("Challenge levels")]
    public List<GameObject> challengeLevels;

    [Tooltip("Number of Enemies to defeat before the challenge is completed")]
    public float numberOfEnemiesToDefeat;

    [Tooltip("Number of Enemies defeated")]
    public float enemiesDefeated;

    [Tooltip("Challenge completed or not")]
    public bool challengeIsCompleted;

    [Tooltip("Texts for the challenge warnings and objectives")]
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI objectiveText;
    
    private int currentWaveIndex = 0;
    private int enemiesInWave = 0;


    private void Start()
    {
        if (challengeLevels.Count > 0)
        {
            enemiesInWave = challengeLevels[currentWaveIndex].transform.childCount;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !challengeIsCompleted)
        {
            // Player has entered the arena
            StartNextWave();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || (other.CompareTag("Boss")) && !challengeIsCompleted)
        {
            enemiesDefeated++;

            if (enemiesDefeated >= numberOfEnemiesToDefeat)
            {
                // All enemies in the current wave are defeated
                CompleteWave();
            }
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < challengeLevels.Count)
        {
            GameObject currentWave = challengeLevels[currentWaveIndex];
            currentWave.SetActive(true);

            enemiesDefeated = 0;
            numberOfEnemiesToDefeat = currentWave.transform.childCount;
            objectiveText.text = "Defeat " + numberOfEnemiesToDefeat + " enemies";

            warningText.gameObject.SetActive(false);
            warningText.gameObject.SetActive(true);
            warningText.text = "Defeat " + numberOfEnemiesToDefeat + " enemies";
            
            
            currentWaveIndex++;
        }
        else
        {
            // All waves completed, you can handle this accordingly
            Debug.Log("All waves completed!");
        }
    }

    private void CompleteWave()
    {
        // Wave completed
        challengeIsCompleted = true;
              
        StartCoroutine(StartNextWaveDelay());
    }

    private IEnumerator StartNextWaveDelay()
    {
        warningText.fontSize = 35;
        objectiveText.text = "Wave completed!";
        warningText.text = "Wave Completed";
        yield return new WaitForSeconds(3f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 20;
        warningText.text = "Next Wave in 5";
        yield return new WaitForSeconds(1.5f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.text = "Next Wave in 4";
        yield return new WaitForSeconds(1.5f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.text = "Next Wave in 3";
        yield return new WaitForSeconds(1.5f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 25;
        warningText.text = "Next Wave in 2";     
        yield return new WaitForSeconds(1.5f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 30;
        warningText.text = "Next Wave in 1";     
        yield return new WaitForSeconds(1.5f);

        StartNextWave();
    }

   
   

}
