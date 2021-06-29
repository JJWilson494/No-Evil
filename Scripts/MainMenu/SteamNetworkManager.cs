using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System;
using UnityEngine.SceneManagement;

public class SteamNetworkManager : NetworkManager
{
    [SerializeField]
    private LobbyPlayerInfoDisplay lobbyPlayerPrefab = null;

    [SerializeField]
    private GameObject gamePlayerPrefab = null;
    [SerializeField]
    private GameObject playerSpawnSystem = null;
    [Scene]
    [SerializeField]
    private string menuScene = string.Empty;

    [Scene]
    [SerializeField]
    private string gameScene = string.Empty;

    protected Callback<LobbyCreated_t> lobbyCreatedCallback;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequestedCallback;
    protected Callback<LobbyEnter_t> lobbyEnterCallback;

    private const string HOST_ADDRESS = "HostAddress";


    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;
    public static event Action OnServerStopped;

    public List<LobbyPlayerInfoDisplay> lobbyPlayers { get; } = new List<LobbyPlayerInfoDisplay>();

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        CSteamID steamID = SteamMatchmaking.GetLobbyMemberByIndex(GameMaster.GetGameMaster().GetLobbyID(), numPlayers - 1);
        Debug.Log("Server Added Player. ID: " + steamID.ToString());
        var lobbyPlayerInfoDisplay = conn.identity.GetComponent<LobbyPlayerInfoDisplay>();

        lobbyPlayerInfoDisplay.SetSteamID(steamID.m_SteamID);

         bool isLeader = lobbyPlayers.Count == 0;
        // LobbyPlayerInfoDisplay lobbyPlayer = Instantiate(lobbyPlayerPrefab);
        // Debug.Log("LobbyPlayer Instantiated: " + lobbyPlayer.gameObject.name);
        lobbyPlayerInfoDisplay.IsLeader = isLeader;
        NetworkServer.AddPlayerForConnection(conn, lobbyPlayerInfoDisplay.gameObject);
        lobbyPlayerInfoDisplay.UpdateDisplay();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<LobbyPlayerInfoDisplay>();
            lobbyPlayers.Remove(player);
            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().maxConnections);
        Debug.Log("Lobby Created");
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            return;
        }

        GameMaster.GetGameMaster().SetLobbyID((CSteamID)callback.m_ulSteamIDLobby);
        Debug.Log("LobbyID Set to " + GameMaster.GetGameMaster().GetLobbyID());
        GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().StartHost();
        SteamMatchmaking.SetLobbyData(GameMaster.GetGameMaster().GetLobbyID(), HOST_ADDRESS, SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEnter(LobbyEnter_t callback)
    {
        if (NetworkServer.active)
        {
            Debug.Log("Lobby Entered - Host");
            return;
        }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HOST_ADDRESS);

        GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().networkAddress = hostAddress;
        GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().StartClient();
        Debug.Log("Client started. Address: " + GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().networkAddress);
    }

    public override void Start()
    {
        base.Start();
        if (!SteamManager.Initialized)
        {
            return;
        }
        Debug.Log("Steam Initialized");
        lobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEnterCallback = Callback<LobbyEnter_t>.Create(OnLobbyEnter);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        ClientScene.AddPlayer(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections || SceneManager.GetActiveScene().name != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnStopServer()
    {
        OnServerStopped?.Invoke();
        lobbyPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in lobbyPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        foreach (var player in lobbyPlayers)
        {
            if (!player.IsReady)
            {
                return false;
            }
        }
        return true;
    }

    public void StartGame()
    {
        if (!IsReadyToStart())
        {
            return;
        }

        ServerChangeScene(gameScene);
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (SceneManager.GetActiveScene().name == menuScene)
        {
            for (int i = lobbyPlayers.Count - 1; i >= 0; i--)
            {
                var conn = lobbyPlayers[i].connectionToClient;
                var gamePlayerInstance = Instantiate(gamePlayerPrefab);

                NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
            }
        }
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == gameScene)
        {
            GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);

        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        OnServerReadied?.Invoke(conn);
    }

}
