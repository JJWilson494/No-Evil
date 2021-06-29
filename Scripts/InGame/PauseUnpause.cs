using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUnpause : MonoBehaviour {

    public GameObject pauseMenu;

    // Use this for initialization
    void Start () 
    {
        Debug.Log("Start called");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameMaster.GetGameMaster().GetPaused())
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (GameMaster.GetGameMaster().GetPaused())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    private void PauseGame()
    {
        GameMaster.GetGameMaster().SetPaused(true);
        pauseMenu.SetActive(true);
        
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {

        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void UnpauseGame()
    {
        GameMaster.GetGameMaster().SetPaused(false);
        pauseMenu.SetActive(false);

        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {

        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }

    public void SettingsMenu()
    {

    }
    
}
