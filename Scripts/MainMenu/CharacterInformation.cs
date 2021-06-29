using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for displaying the character information during the character select screen
/// </summary>
public class CharacterInformation 
{
    //Name for this character
    private string m_name;

    //Description of the character
    private string m_desc;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the character</param>
    /// <param name="desc">The description of the character</param>
    public CharacterInformation(string name, string desc)
    {
        m_name = name;
        m_desc = desc;
    }

    /// <summary>
    /// Gets the name of the character
    /// </summary>
    /// <returns>The name of the character</returns>
    public string GetName()
    {
        return m_name;
    }

    /// <summary>
    /// Gets the description of the character
    /// </summary>
    /// <returns>The description of the character</returns>
    public string GetDesc()
    {
        return m_desc;
    }
}
