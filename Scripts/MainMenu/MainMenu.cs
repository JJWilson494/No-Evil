using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Main menu for the game
/// </summary>
public class MainMenu : MonoBehaviour 
{
    /// <summary>
    /// Initialize the timescale so the animations play on startup
    /// </summary>
    void Start()
    {
        Time.timeScale = 1f;
        GameObject version = GameObject.Find("VersionNumber");
        TextMeshProUGUI versionText = version.GetComponent<TextMeshProUGUI>();
        versionText.text = Application.version;
    }

    /// <summary>
    /// Exit the game
    /// </summary>
	public void QuitGame()
    {
        Application.Quit();
    }

}
