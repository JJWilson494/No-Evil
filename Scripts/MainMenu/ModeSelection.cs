using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelection : MonoBehaviour {

    public void LaunchMultiplayer()
    {
        SceneManager.LoadScene("MultiplayerLobby");
    }
}
