using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartGame : MonoBehaviour {

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Transform spawnPoint;

	// Use this for initialization
	 void Start ()
    {
     //   if (!GameStatus.isMultiplayer)
        {
           // Instantiate(playerPrefab, new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), Quaternion.identity);
        }
    }

	
}
