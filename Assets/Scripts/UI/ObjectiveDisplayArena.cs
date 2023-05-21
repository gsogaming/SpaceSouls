using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class inherits for the UIelement class and handles updating the score display
/// </summary>
public class ObjectiveDisplayArena : UIelement
{
    [Tooltip("The text UI to use for display")]
    public TextMeshProUGUI displayText = null;
    private GameManagerArena gameManagerArena;

    private void Awake()
    {
        gameManagerArena = FindObjectOfType<GameManagerArena>();
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
        if (displayText != null && gameManagerArena.bossesToDefeat > gameManagerArena.bossesDefeated)
        {
            displayText.text = "Objectives: Kill " +
                (gameManagerArena.bossesToDefeat - gameManagerArena.bossesDefeated) + " boss(es) to finish level";                 
        }

        if (displayText != null && gameManagerArena.bossesToDefeat == gameManagerArena.bossesDefeated && gameManagerArena.enemiesDefeated < gameManagerArena.enemiesToDefeat)
        {
            displayText.text = "Objectives: Kill " + (gameManagerArena.enemiesToDefeat - gameManagerArena.enemiesDefeated) + " enemies to finish level";
        }
        if (displayText != null && gameManagerArena.bossesDefeated >= gameManagerArena.bossesToDefeat && gameManagerArena.enemiesDefeated >= gameManagerArena.enemiesToDefeat)
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
