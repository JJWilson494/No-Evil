using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SpawnItems : NetworkBehaviour
{
    private List<GameObject> objectiveItems;

    public Material[] mats;

    public GameObject prefab;

    public Transform[] loca1Spawns;
    public Transform[] loca2Spawns;
    public Transform[] loca3Spawns;
    public Transform[] loca4Spawns;
    public Transform[] loca5Spawns;
    public Transform[] loca6Spawns;
    public Transform[] loca7Spawns;



    // Use this for initialization
    void Start ()
    {
        if (GameMaster.GetGameMaster().isMultiplayer)
        {
            ClientScene.RegisterPrefab(prefab);
            RpcSpawnCollectibles();
        }
        else
        {
            offlineSpawnItems();
        }
        
    }
    [ClientRpc]
    void RpcSpawnCollectibles()
    {

        List<Material> matList = new List<Material>(mats);
        objectiveItems = new List<GameObject>();

        int spawnLocation = Random.Range(0, loca1Spawns.Length);
        Transform position = loca1Spawns[spawnLocation];
        GameObject newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        Renderer rend = newItem.GetComponent<Renderer>();
        int mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().isMultiplayer)
        {
            NetworkServer.Spawn(newItem);

        }

        spawnLocation = Random.Range(0, loca2Spawns.Length);
        position = loca2Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
            NetworkServer.Spawn(newItem);
        }


        spawnLocation = Random.Range(0, loca3Spawns.Length);
        position = loca3Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
            NetworkServer.Spawn(newItem);
        }

        spawnLocation = Random.Range(0, loca4Spawns.Length);
        position = loca4Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
            NetworkServer.Spawn(newItem);
        }

        spawnLocation = Random.Range(0, loca5Spawns.Length);
        position = loca5Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
            NetworkServer.Spawn(newItem);
        }

        spawnLocation = Random.Range(0, loca6Spawns.Length);
        position = loca6Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
            NetworkServer.Spawn(newItem);
        }

        spawnLocation = Random.Range(0, loca7Spawns.Length);
        position = loca7Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        if (GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
            NetworkServer.Spawn(newItem);
        }
    }

    void offlineSpawnItems()
    {
        List<Material> matList = new List<Material>(mats);
        objectiveItems = new List<GameObject>();

        int spawnLocation = Random.Range(0, loca1Spawns.Length);
        Transform position = loca1Spawns[spawnLocation];
        GameObject newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        Renderer rend = newItem.GetComponent<Renderer>();
        int mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        

        spawnLocation = Random.Range(0, loca2Spawns.Length);
        position = loca2Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        


        spawnLocation = Random.Range(0, loca3Spawns.Length);
        position = loca3Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        

        spawnLocation = Random.Range(0, loca4Spawns.Length);
        position = loca4Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        

        spawnLocation = Random.Range(0, loca5Spawns.Length);
        position = loca5Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        

        spawnLocation = Random.Range(0, loca6Spawns.Length);
        position = loca6Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
        

        spawnLocation = Random.Range(0, loca7Spawns.Length);
        position = loca7Spawns[spawnLocation];
        newItem = Instantiate(prefab, new Vector3(position.position.x, position.position.y, position.position.z), Quaternion.identity);
        rend = newItem.GetComponent<Renderer>();
        mat = Random.Range(0, matList.Count);
        rend.material = matList[mat];
        rend.material.SetOverrideTag("MatTag", "Item");
        matList.RemoveAt(mat);
        objectiveItems.Add(newItem);
       
    }
}


