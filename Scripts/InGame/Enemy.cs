using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform enemy;

    private bool ShouldMove;

    private Camera mainCam;

    private bool playerExists;

    private List<EnemyStatus> enemyStates;

    private List<AudioSource> audioSources;

    // Use this for initialization
    void Start()
    {
        ShouldMove = true;
        mainCam = Camera.main;
        playerExists = false;
        enemyStates = new List<EnemyStatus>();
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
        initializeStates();
    }

    private void initializeStates()
    {
        EnemyStatus collected_0 = new EnemyStatus(10, 60, false);
        EnemyStatus collected_1 = new EnemyStatus(9, 45, false);
        EnemyStatus collected_2 = new EnemyStatus(8, 30, false);
        EnemyStatus collected_3 = new EnemyStatus(6, 20, false);
        EnemyStatus collected_4 = new EnemyStatus(5, 15, false);
        EnemyStatus collected_5 = new EnemyStatus(3, 10, true);
        EnemyStatus collected_6 = new EnemyStatus(2, 5, true);
        enemyStates.Add(collected_0);
        enemyStates.Add(collected_1);
        enemyStates.Add(collected_2);
        enemyStates.Add(collected_3);
        enemyStates.Add(collected_4);
        enemyStates.Add(collected_5);
        enemyStates.Add(collected_6);

    }

    private bool IsVisible()
    {
        GameObject enemyObj = GameObject.FindWithTag("Enemy");
        bool visibility = enemyObj.GetComponentInChildren<Renderer>().isVisible;
        RaycastHit hit = new RaycastHit();
        if (visibility)
        {
            Physics.Linecast(enemy.transform.position, player.position, out hit);
            visibility = (hit.collider.tag == ("Player"));
            if (visibility)
            {
                float dist = Vector3.Distance(player.position, enemy.position);
                visibility = dist < 20.0f;
            }
        }
        return visibility;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerExists)
        {
            try
            {
                player = GameObject.FindWithTag("Player").transform;
                playerExists = true;
            }
            catch (NullReferenceException e)
            {

            }
        }
        if (ShouldMove && playerExists)
        {
            player = GameObject.FindWithTag("Player").transform;
            int collected = GameMaster.GetGameMaster().collectedCount;
            EnemyStatus currentState;
            if (!IsVisible())
            {
                if (enemyStates.Count != 7)
                {
                    currentState = new EnemyStatus(10, 60, false);
                }
                else
                {
                    currentState = enemyStates[collected];
                }
                StartCoroutine(Teleport(currentState.distance, currentState.frequency));
            }
        }

    }

    private class EnemyStatus
    {
        public int distance;
        public int frequency;
        public bool chase;

        public EnemyStatus(int distance_in, int frequency_in, bool chase_in)
        {
            distance = distance_in;
            frequency = frequency_in;
            chase = chase_in;
        }
    }



    IEnumerator Teleport(int distance, int frequency)
    {
        ShouldMove = false;
        Vector3 backVector = player.forward * distance;
        backVector.y = player.position.y;
        enemy.position = player.position - backVector;
        enemy.LookAt(player);
        Quaternion enemyRot = enemy.rotation;
        enemyRot.x = 0;
        enemy.rotation = enemyRot;
        int index = UnityEngine.Random.Range(0, audioSources.Count);
        audioSources[index].Play();
        yield return new WaitForSeconds(frequency);
        ShouldMove = true;
    }
}
