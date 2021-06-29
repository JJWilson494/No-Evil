using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] disableComponents;

    [SerializeField]
    private Canvas ui;

    private Camera sceneCamera;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Canvas is:" + ui.enabled);
        Debug.Log("Local Player: " + isLocalPlayer);
		//if (!isLocalPlayer)
      //  {
       //     for (int x = 0; x < disableComponents.Length; ++x)
        //    {
        //        disableComponents[x].enabled = false;
        //    }

      //      ui.enabled = false;
        //    Debug.Log("Canvas for non local player is:" + ui.enabled);

       // }
       // else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            ui.enabled = true;
            Debug.Log("Canvas for local player is:" + ui.enabled);

        }
    }

    void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
	
}
