using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Script for modifying the display and volume settings
/// </summary>
public class SettingsMenu : MonoBehaviour {

    //Main audio volume mixer
    [SerializeField]
    private AudioMixer m_audioMixer;

    //Name of the volume float
    private static string VOLUME_NAME = "volume";

    //All valid resolutions for this display
    private Resolution[] m_validResolutions;

    //UI element for the resolution dropdown
    [SerializeField]
    private Dropdown m_resolutionDropdown;

    //Toggle option for fullscreen
    [SerializeField]
    private Toggle m_fullScreenToggle;

    /// <summary>
    /// Initialization of settings options
    /// </summary>
    public void Start()
    {
        //Initialize base settings
        m_validResolutions = Screen.resolutions;
        m_fullScreenToggle.GetComponent<Toggle>().isOn = Screen.fullScreen;
        m_resolutionDropdown.ClearOptions();

        //List used to display the string values for each resolution in the dropdown
        List<string> resolutionStrings = new List<string>();
        int currentResIndex = 0;

        //Add each resolution to the list
        for (int x = 0; x < m_validResolutions.Length; x++)
        {
            string option = m_validResolutions[x].width + " x " + m_validResolutions[x].height + " @ " + m_validResolutions[x].refreshRate + "Hz";
            resolutionStrings.Add(option);

            //Determine if this is the current screen resolution and set that
            if (m_validResolutions[x].width == Screen.width &&
                m_validResolutions[x].height == Screen.height)
            {
                currentResIndex = x;
            }
            
        }
        m_resolutionDropdown.AddOptions(resolutionStrings);
        m_resolutionDropdown.value = currentResIndex;
        m_resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Function called when the volume slider is adjusted
    /// </summary>
    /// <param name="volume"> New volume value </param>
    public void SetVolume (float volume)
    {
        m_audioMixer.SetFloat(VOLUME_NAME, volume);
    }

    /// <summary>
    /// Set the quality level integer when the dropdown for graphics is changed
    /// </summary>
    /// <param name="quality"> The integer level of quality for the game </param>
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    /// <summary>
    /// Fullscreen toggle. Called when the toggle box is unchecked
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    /// <summary>
    /// Adjust the resolution when the dropdown is modified
    /// </summary>
    /// <param name="resolutionIndex"> New resolution to assign </param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = m_validResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

}
