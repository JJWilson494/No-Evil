using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudio : MonoBehaviour
{

    private List<AudioSource> audioSources;

    private bool isPlaying;

    private AudioSource selectedSource;

	// Use this for initialization
	void Start () {
        audioSources = new List<AudioSource>();
        audioSources.AddRange(GetComponents<AudioSource>());
        isPlaying = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (selectedSource == null || !selectedSource.isPlaying)
        {
            float random = Random.Range(0, 1000.0f);
            
            if (random < 0.5f)
            {
                Debug.Log("Random is: " + random);
                Debug.Log("Playing audio");
                int index = Random.Range(0, audioSources.Count);
                selectedSource = audioSources[index];
                if (!isPlaying)
                {
                    selectedSource.Play();
                    StartCoroutine(WaitForAudio());
                }
            }
        }
    }

    IEnumerator WaitForAudio()
    {
        isPlaying = true;
        yield return new WaitForSecondsRealtime(60);
        isPlaying = false;
    }
}
