using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;

/// <summary>
/// Main game master class for handling the game status
/// </summary>
public class GameMaster : NetworkBehaviour
{
    //List of menu players for transitioning to the game scene
    public List<MenuPlayerInformation> players = new List<MenuPlayerInformation>();

    //Is multiplayer flag
    public bool isMultiplayer;

    //Is paused flag
    private bool isPaused;

    //IP Address of the server
    private string ipAddress;

    //Port of the server
    private string port;

    //Count of collected relics
    [SyncVar]
    public int collectedCount = 0;

    //End game status
    private bool victoryStatus;

    //Game master singleton
    private static GameMaster m_gameMaster;

    //Steam lobby ID
    private CSteamID m_lobbyID;

    /// <summary>
    /// Function when the game is first started. Creates the singleton reference
    /// </summary>
    void Awake()
    {
        InstantiateGameMaster();
    }

    /// <summary>
    /// Initialization of the gamemaster object
    /// </summary>
    private void InstantiateGameMaster()
    {
        if (m_gameMaster == null)
        {
            m_gameMaster = this;
            DontDestroyOnLoad(this);
        }
        else if (this != m_gameMaster)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Getter for the singleton reference
    /// </summary>
    /// <returns>The gamemaster singleton</returns>
    public static GameMaster GetGameMaster()
    {
        return m_gameMaster;
    }

    /// <summary>
    /// Set pause flag
    /// </summary>
    /// <param name="status">The pause status</param>
    public void SetPaused(bool status)
    {
        isPaused = status;
    }

    /// <summary>
    /// Gets the list of menu players in the lobby
    /// </summary>
    /// <returns>The backing list of menu players</returns>
    public List<MenuPlayerInformation> GetPlayers()
    {
        return players;
    }

    /// <summary>
    /// Gets the pause status
    /// </summary>
    /// <returns>The flag representing pause</returns>
    public bool GetPaused()
    {
        return isPaused;
    }

    /// <summary>
    /// Gets the IP address of the server
    /// </summary>
    /// <returns>The IP address of the server</returns>
    public string GetIPAddress()
    {
        return ipAddress;
    }

    /// <summary>
    /// Gets the port of the server
    /// </summary>
    /// <returns></returns>
    public string GetPort()
    {
        return port;
    }

    /// <summary>
    /// Sets the IP address of the server
    /// </summary>
    /// <param name="addr">The IP address</param>
    public void SetIPAddress(string addr)
    {
        ipAddress = addr;
    }

    /// <summary>
    /// Sets the port of the server
    /// </summary>
    /// <param name="p">The port of the server</param>
    public void SetPort(string p)
    {
        port = p;
    }

    /// <summary>
    /// Collects the item. Triggered on collision with a relic.
    /// </summary>
    public void CollectItem()
    {
        collectedCount++;
        if (collectedCount == 7)
        {
            EndGame(true);
        }
    }

    /// <summary>
    /// Sets the multiplayer status
    /// </summary>
    /// <param name="status">Sets the multiplayer status</param>
    public void SetMultiplayerStatus(bool status)
    {
        isMultiplayer = status;
    }

    /// <summary>
    /// Gets the victory status
    /// </summary>
    /// <returns>The victory status. True if win, false if lost</returns>
    public bool GetVictoryStatus()
    {
        return victoryStatus;
    }

    /// <summary>
    /// Sets the victory status
    /// </summary>
    /// <param name="status">True if win, false if lost</param>
    public void SetVictoryStatus(bool status)
    {
        victoryStatus = status;
    }

    /// <summary>
    /// End the game and load the scene
    /// </summary>
    /// <param name="victory">True if victory, false if lost</param>
    private void EndGame(bool victory)
    {
        SetVictoryStatus(victory);
        SceneManager.LoadScene("EndGame");
    }
    /// <summary>
    /// Gets the multiplayer status
    /// </summary>
    /// <returns>True if in multiplayer</returns>
    public bool GetMultiplayerStatus()
    {
        return isMultiplayer;
    }

    /// <summary>
    /// Sets the steam lobby ID
    /// </summary>
    /// <param name="id">The steam ID of the lobby</param>
    public void SetLobbyID(CSteamID id)
    {
        m_lobbyID = id;
    }

    /// <summary>
    /// Gets the steam ID of the lobby
    /// </summary>
    /// <returns>The lobby steam ID</returns>
    public CSteamID GetLobbyID()
    {
        return m_lobbyID;
    }
}

