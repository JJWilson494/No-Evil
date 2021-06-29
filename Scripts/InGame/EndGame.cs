using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script for ending the game
/// </summary>
public class EndGame : MonoBehaviour {

    //The victory status. True for success, false for failure
    private bool victory;

    //Reference to title text
    [SerializeField]
    private TextMeshProUGUI title;

    //Reference to description text
    [SerializeField]
    private TextMeshProUGUI desc;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        victory = GameMaster.GetGameMaster().GetVictoryStatus();
        if (!victory)
        {
            if (GameMaster.GetGameMaster().isMultiplayer)
            {
                title.text = "Failure";
                desc.text = "Your team has failed to escape";
            }
            else
            {
                title.text = "Failure";
                desc.text = "You have failed to escape";            
            }
            
        }
	}
	
    /// <summary>
    /// Restart the game
    /// </summary>
    public void restartGame()
    {
        //Gotta be a better way to reset back to main scene and reset info
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void quit()
    {
        Application.Quit();
    }
}
