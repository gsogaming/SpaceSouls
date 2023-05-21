using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArenaManager : MonoBehaviour
{


    [Tooltip("Challenge levels' list")]
    public List<GameObject> challengeLevels;

    [Tooltip("Container of the current level")]
    public GameObject currentLevelPrefab;

    [Tooltip("Number of Enemies to defeat before the challenge is completed")]
    public float numberOfEnemiesToDefeat;

    [Tooltip("Number of Enemies defeated")]
    public float enemiesDefeated;

    [Tooltip("Number of Bosses defeated")]
    public float bossesDefeated;

    [Tooltip("Challenge completed or not")]
    public bool challengeIsCompleted;

    [Tooltip("Texts for the challenge warnings and objectives")]
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI objectiveText;

    private int currentWaveIndex = 0;

    public int numberOfBosses;
    private int numberOfEnemies;
    public int numberOfRegularEnemies;


    private void Start()
    {

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
        if (other.CompareTag("Enemy"))
        {
            enemiesDefeated++;
        }
        else if (other.CompareTag("Boss"))
        {
            bossesDefeated++;
        }

        if (bossesDefeated >= numberOfBosses && enemiesDefeated >= numberOfEnemiesToDefeat && !challengeIsCompleted)
        {
            // All enemies in the current wave are defeated
            CompleteWave();
            
        }
    }

    private void StartNextWave()
    {
        challengeIsCompleted = false;
        if (currentWaveIndex < challengeLevels.Count)
        {
            // Deactivate the previous wave
            if (currentLevelPrefab != null)
            {
                currentLevelPrefab.SetActive(false);
            }

            currentLevelPrefab = challengeLevels[currentWaveIndex];
            currentLevelPrefab.SetActive(true);

            enemiesDefeated = 0;
            numberOfEnemies = currentLevelPrefab.transform.childCount;
            numberOfBosses = CountBossesInWave(currentLevelPrefab);
            numberOfRegularEnemies = numberOfEnemies - numberOfBosses;

            if (numberOfBosses > 0)
            {

                numberOfEnemiesToDefeat = currentWaveIndex +1  + numberOfBosses;
                objectiveText.text = "Defat " + numberOfBosses + " bosses" + " and " + currentWaveIndex + " enemies";
                warningText.text = "Defat " + numberOfBosses + " bosses  and " + currentWaveIndex * 1 + " enemies";
            }
            else
            {

                numberOfEnemiesToDefeat = currentWaveIndex + 1;
                objectiveText.text = "Defeat " + currentWaveIndex + " enemies";
                warningText.text = "Defeat " + currentWaveIndex  + " enemies";
            }

            warningText.fontSize = 20;
            warningText.gameObject.SetActive(false);
            warningText.gameObject.SetActive(true);


            currentWaveIndex++;
            
        }
        else
        {
            // All waves completed, you can handle this accordingly
            Debug.Log("All waves completed!");
        }
    }

    private int CountBossesInWave(GameObject wave)
    {
        int bossCount = 0;
        foreach (Transform enemy in wave.transform)
        {
            if (enemy.CompareTag("Boss"))
            {
                bossCount++;
            }
        }
        return bossCount;
    }

    private void CompleteWave()
    {
        // Wave completed
        challengeIsCompleted = true;
        currentLevelPrefab.SetActive(false);
        numberOfEnemiesToDefeat = 0;
        StartCoroutine(StartNextWaveDelay());
    }

    private IEnumerator StartNextWaveDelay()
    {

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 35;
        objectiveText.text = "Wave completed!";
        warningText.text = "Wave Completed";
        yield return new WaitForSeconds(3f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 20;
        warningText.text = "Next Wave in 5";
        yield return new WaitForSeconds(1f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.text = "Next Wave in 4";
        yield return new WaitForSeconds(1f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.text = "Next Wave in 3";
        yield return new WaitForSeconds(1f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 50;
        warningText.text = "Next Wave in 2";
        yield return new WaitForSeconds(1f);

        warningText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(true);
        warningText.fontSize = 70;
        warningText.text = "Next Wave in 1";
        yield return new WaitForSeconds(1f);

        StartNextWave();
    }




}
