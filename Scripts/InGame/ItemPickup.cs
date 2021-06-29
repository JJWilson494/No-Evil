using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemPickup : MonoBehaviour {

    public AudioSource[] pickupAudio;

    private List<AudioSource> audios;

    void Start()
    {
        audios = new List<AudioSource>(pickupAudio);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.name == "Collectible(Clone)")
        {
            GameMaster.GetGameMaster().CollectItem();
            Destroy(col.gameObject);
            int audioFile = Random.Range(0, audios.Count);
            pickupAudio[audioFile].Play();
            audios.RemoveAt(audioFile);
        }
    }
}
