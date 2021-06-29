using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using Steamworks;

public class LobbyPlayerInfoDisplay : NetworkBehaviour
{
    [SerializeField]
    private GameObject m_lobbyUI = null;
    [SerializeField]
    private TMP_Text[] playerReadyTexts = new TMP_Text[3];
    [SyncVar(hook = nameof(HandleSteamIDUpdated))]
    private ulong m_steamID;
    [SerializeField]
    private RawImage m_profileImage;
    [SerializeField]
    private TMP_Text m_displayName;
    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
    public bool IsReady = false;
    protected Callback<AvatarImageLoaded_t> avatarImageLoadedCallback;
    private bool isLeader;
    public bool IsLeader
    {
        set
        {
            isLeader = value;
            //Enable Start Game button
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Server

    public void SetSteamID(ulong steamID)
    {
        m_steamID = steamID;
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        avatarImageLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
        GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().lobbyPlayers.Add(this);
        Debug.Log("Adding player");
        UpdateDisplay();
    }

    private void HandleSteamIDUpdated(ulong oldSteamID, ulong newSteamID)
    {
        var steamID = new CSteamID(newSteamID);
        m_displayName.text = SteamFriends.GetFriendPersonaName(steamID);

        int imageID = SteamFriends.GetLargeFriendAvatar(steamID);

        if (imageID == -1)
        {
            return;
        }
        else
        {
            m_profileImage.texture = GetSteamImageAsTexture(imageID);
        }
    }

    public void HandleReadyStatusChanged(bool oldValue, bool newValue)
    {
        UpdateDisplay();
    }

    public void HandleReadyToStart(bool readyToStart)
    {
        if (!isLeader) { return; }

        //startGameButton.interactable = readyToStart;
    }

    private Texture2D GetSteamImageAsTexture(int image)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(image, out uint width, out uint height);

        if (isValid)
        {
            byte[] byteImage = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(image, byteImage, (int)(width * height * 4));
            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(byteImage);
                texture.Apply();
            }
        }

        return texture;
    }

    private void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
    {
        if (callback.m_steamID.m_SteamID == m_steamID)
        {
            return;
        }

        m_profileImage.texture = GetSteamImageAsTexture(callback.m_iImage);
    }

    public override void OnStartAuthority()
    {
        m_lobbyUI.SetActive(true);
    }

    public void UpdateDisplay()
    {
        if (!hasAuthority)
        {
            foreach (var player in GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().lobbyPlayers)
            {
                if (player.isLocalPlayer)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }
        for (int i = 0; i < SteamMatchmaking.GetNumLobbyMembers(GameMaster.GetGameMaster().GetLobbyID()); i++)
        {
            int offByOne = i + 1;
            string gameObjectName = "Player" + offByOne;
            GameObject player = GameObject.Find(gameObjectName);
            CSteamID lobbyId = GameMaster.GetGameMaster().GetLobbyID();
            CSteamID lobbyMember = SteamMatchmaking.GetLobbyMemberByIndex(lobbyId, i);
            int imageID = SteamFriends.GetLargeFriendAvatar(lobbyMember);
            if (player == null)
            {
                return;
            }
            if (imageID != -1)
            {
                player.GetComponentInChildren<RawImage>().texture = GetSteamImageAsTexture(imageID);
            }
            player.GetComponentInChildren<TMP_Text>().text = SteamFriends.GetFriendPersonaName(lobbyMember);
            Debug.Log("At area to set text. Ready Status: " + GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().lobbyPlayers[i].IsReady);
            playerReadyTexts[i].text = GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().lobbyPlayers[i].IsReady ? "<color=green>Ready</color>" : "<color=red>Not Ready</color>";
        }


    }

    [Command]
    public void CmdReadyUp()
    {
        IsReady = !IsReady;
        GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().NotifyPlayersOfReadyState();
    }

    [Command]
    public void CmdStartGame()
    {
        if (GameMaster.GetGameMaster().gameObject.GetComponent<SteamNetworkManager>().lobbyPlayers[0].connectionToClient != connectionToClient)
        {
            return;
        }
    }

    #endregion


}
