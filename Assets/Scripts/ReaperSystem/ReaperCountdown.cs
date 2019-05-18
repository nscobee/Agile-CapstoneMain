using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReaperCountdown : MonoBehaviour
{
    public bool outOfBody;
    public bool reaperHasSpawned = false;
    public bool allowedToSpawn = false;

    public GameObject phantom;
    public GameObject reaperPrefab;
    public GameObject currentReaper;
    public Transform reaperSpawnPoint;

    public float countDownTime = 0;
    public float timeTillReaperSpawn;
    private float reaperSpawnsAfterInSecs;
    public float reaperSpawnMultiplier = 1;

    public float despawnTime = 0;
    public float timeTillDespawn;
    public float reaperDespawnMultiplier = 1;

    public float countdownTimerReset = 0;
    public float timeTillCountdownReset;
    public float timerResetMultiplier = 1;




    private void Start()
    {
        if (phantom.GetComponent<PhantomControls>().isPossessing) outOfBody = false;
        else outOfBody = true;
        reaperSpawnsAfterInSecs = timeTillReaperSpawn;
    }


    void Update()
    {
        if (outOfBody && !reaperHasSpawned)
        {
            countDownTime += Time.deltaTime * reaperSpawnMultiplier;
        }

        if (!outOfBody && reaperHasSpawned)
        {
            despawnTime += Time.deltaTime * reaperDespawnMultiplier;
        }

        if (countDownTime >= timeTillReaperSpawn)
        {
            if (!reaperHasSpawned)
            {
                SpawnReaper();
            }
            
            reaperHasSpawned = true;
            countDownTime = 0;
        }

        if (despawnTime >= timeTillDespawn)
        {
            DespawnReaper();
            reaperHasSpawned = false;
            despawnTime = 0;
        }

        if (!outOfBody && !reaperHasSpawned && countDownTime > 0)
        {
            countdownTimerReset += Time.deltaTime * timerResetMultiplier;
        }

        if (countdownTimerReset > timeTillCountdownReset)
        {
            countDownTime = 0;
            countdownTimerReset = 0;
        }

        if ((SceneManager.GetSceneByBuildIndex(1) == SceneManager.GetActiveScene()) && !allowedToSpawn)
        {
            timeTillReaperSpawn = 90000;
        }
        else timeTillReaperSpawn = reaperSpawnsAfterInSecs;

    }


    private void DespawnReaper()
    {
        Destroy(GameObject.FindWithTag("Reaper"));
    }

    public void SpawnReaper()
    {
        Instantiate(currentReaper, reaperSpawnPoint.position, Quaternion.identity, null);
    }
}
