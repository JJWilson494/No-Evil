using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

/// <summary>
/// Backing script for panel regarding player selection
/// </summary>
public class CharacterSelect : MonoBehaviour {

    //Label for the character name
    [SerializeField]
    private TextMeshProUGUI m_name;

    //Label for the character description
    [SerializeField]
    private TextMeshProUGUI m_desc;

    //Position in the list to display
    private int m_position;

    //List of available characters
    private List<CharacterInformation> m_characters = new List<CharacterInformation>();

    //Reference to GameMaster
    [SerializeField]
    private GameMaster m_gameMaster;

    /// <summary>
    /// Initialize the characters that are available for selection.
    /// </summary>
	void Start ()
    {
        CharacterInformation see = new CharacterInformation("See", "This person has lost their sight. However, they have developed an echolocation like system and must move by making sounds and listening.");
        CharacterInformation hear = new CharacterInformation("Hear", "This person has lost their hearing. Things are slightly brighter for this person.");
        CharacterInformation speak = new CharacterInformation("Speak", "This person has lost their voice. They must communicate using sign language. They can better hear the monster's location. ");

        m_characters.Add(see);
        m_characters.Add(hear);
        m_characters.Add(speak);

        m_position = 0;
        SetCharacterText(m_position);
    }

    /// <summary>
    /// Updates the display based on the position of the character selection
    /// </summary>
    /// <param name="pos">The index in the list to display</param>
    private void SetCharacterText(int pos)
    {
        CharacterInformation currCharater = m_characters[pos];
        m_name.text = currCharater.GetName();
        m_desc.text = currCharater.GetDesc();
    }

    /// <summary>
    /// Moves the index to the right and updates the display
    /// </summary>
    public void MoveRight()
    {
        m_position += 1;
        if (m_position > m_characters.Count - 1)
        {
            m_position = 0;
        }
        SetCharacterText(m_position);
    }

    /// <summary>
    /// Moves the index to the left and updates the display
    /// </summary>
    public void MoveLeft()
    {
        m_position -= 1;
        if (m_position < 0)
        {
            m_position = m_characters.Count - 1;
        }
        SetCharacterText(m_position);
    }

    /// <summary>
    /// Select the given character and start the game
    /// </summary>
    public void Select()
    {
       MenuPlayerInformation playerInfo = new MenuPlayerInformation();
       playerInfo.SetName("SteamName");
       playerInfo.SetCharacterSelected(m_position);
       playerInfo.SetCharacterIndex(1);
       m_gameMaster.players.Add(playerInfo);
       SceneManager.LoadScene("GameLevel");
        
    }

    private void SetupServer()
    {
      //  NetworkServer.Listen(4444);
    }
}
