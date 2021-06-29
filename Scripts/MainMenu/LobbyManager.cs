using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Steamworks;
using UnityEngine.UI;
using Mirror;

public class LobbyManager : MonoBehaviour
{
    private static NetworkManager m_networkManager;

    protected Callback<LobbyCreated_t> lobbyCreatedCallback;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequestedCallback;
    protected Callback<LobbyEnter_t> lobbyEnterCallback;

    private const string HOST_ADDRESS = "HostAddress";

    void Start()
    {
        if (!SteamManager.Initialized)
        {
            return;
        }
        Debug.Log("Steam Initialized");
        lobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEnterCallback = Callback<LobbyEnter_t>.Create(OnLobbyEnter);
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
}
