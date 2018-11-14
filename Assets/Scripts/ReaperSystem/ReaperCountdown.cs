using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperCountdown : MonoBehaviour
{
  public bool outOfBody;
    public bool reaperHasSpawned = false;

    public GameObject phantom;

  public GameObject reaperPrefab;
  public GameObject currentReaper;
    public Transform reaperSpawnPoint;

    public float countDownTime = 0;
    public float timeTillReaperSpawn;
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

    }
  

  void Update ()
  {
        if (outOfBody && !reaperHasSpawned)
        {
            countDownTime += Time.deltaTime * reaperSpawnMultiplier;
        }

        if(!outOfBody && reaperHasSpawned)
        {
            despawnTime += Time.deltaTime * reaperDespawnMultiplier;
        }

        if(countDownTime >= timeTillReaperSpawn)
        {
            SpawnReaper();
            reaperHasSpawned = true;
            countDownTime = 0;            
        }

        if(despawnTime >= timeTillDespawn)
        {
            DespawnReaper();
            reaperHasSpawned = false;
            despawnTime = 0;            
        }

        if(!outOfBody && !reaperHasSpawned && countDownTime > 0)
        {
            countdownTimerReset += Time.deltaTime * timerResetMultiplier;
        }

        if(countdownTimerReset > timeTillCountdownReset)
        {
            countDownTime = 0;
            countdownTimerReset = 0;
        }
        
  }


  private void DespawnReaper()
  {
        Destroy(GameObject.FindWithTag("Reaper"));
  }

  private void SpawnReaper()
  {
        Instantiate(currentReaper, reaperSpawnPoint);
  }
}
