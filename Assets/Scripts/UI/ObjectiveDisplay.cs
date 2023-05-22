using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class inherits for the UIelement class and handles updating the score display
/// </summary>
public class ObjectiveDisplay : UIelement
{
    [Tooltip("The text UI to use for display")]
    public TextMeshProUGUI displayText = null;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        DisplayObjective();
    }

    /// <summary>
    /// Description:
    /// Updates the score display
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public void DisplayObjective()
    {
        if (displayText != null && gameManager.bossesToDefeat > gameManager.bossesDefeated)
        {
            displayText.text = "Objectives: Kill " +
                (gameManager.bossesToDefeat - gameManager.bossesDefeated) + " boss(es)";                 
        }

        if (displayText != null && gameManager.bossesToDefeat == gameManager.bossesDefeated && gameManager.enemiesDefeated < gameManager.enemiesToDefeat)
        {
            displayText.text = "Objectives: Kill " + (gameManager.enemiesToDefeat - gameManager.enemiesDefeated) + " enemies to finish level";
        }
        if (displayText != null && gameManager.bossesDefeated >= gameManager.bossesToDefeat && gameManager.enemiesDefeated >= gameManager.enemiesToDefeat)
        {
            displayText.text = "Objectives Completed";
        }
        
    }

    /// <summary>
    /// Description:
    /// Overides the virtual UpdateUI function and uses the DisplayScore to update the score display
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    public override void UpdateUI()
    {
        // This calls the base update UI function from the UIelement class
        base.UpdateUI();

        // The remaining code is only called for this sub-class of UIelement and not others
        DisplayObjective();
    }
}
