using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class storing information about the menu player
/// </summary>
public class MenuPlayerInformation {

    /// <summary>
    /// The character selected
    /// </summary>
    private int m_characterSelected;

    /// <summary>
    /// Name of the selected character
    /// </summary>
    private string m_name;

    /// <summary>
    /// Index of the selected character
    /// </summary>
    private int m_characterIndex;

    /// <summary>
    /// Gets the selected character
    /// </summary>
    /// <returns>The selected character</returns>
    public int GetSelectedCharacter()
    {
        return m_characterSelected;
    }

    /// <summary>
    /// Gets the name of the selected character
    /// </summary>
    /// <returns>The selected character name</returns>
    public string GetName()
    {
        return m_name;
    }

    /// <summary>
    /// Sets the selected character
    /// </summary>
    /// <param name="selected">The selected character</param>
    public void SetCharacterSelected(int selected)
    {
        m_characterSelected = selected;
    }

    /// <summary>
    /// Sets the selected character name
    /// </summary>
    /// <param name="name">The name of the character</param>
    public void SetName(string name)
    {
        m_name = name;
    }

    /// <summary>
    /// Gets the selected character index
    /// </summary>
    /// <returns>The character index</returns>
    public int GetCharacterIndex()
    {
        return m_characterIndex;
    }

    /// <summary>
    /// Sets the selected character index
    /// </summary>
    /// <param name="index">The index of the character</param>
    public void SetCharacterIndex(int index)
    {
        m_characterIndex = index;
    }
}
