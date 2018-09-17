using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperCountdown : MonoBehaviour
{
  public bool outOfBody;

  public static bool allowReaper = false;

  public GameObject reaperPrefab;
  public GameObject currentReaper;

  private float _countDownTime = 0;
  public float CountDownTime
  {
    get { return _countDownTime; }


    set
    {
      if (value >= 0)
      {
        _countDownTime = value;
      }
      else
      {
        _countDownTime = 0;
      }
    }
  }

  private void Start()
  {
    PhantomControls.reaper = this;

  }

  void Update ()
  {
    if (allowReaper)
    {
      CheckToSpawnReaper();
      CheckToDespawnReaper();
      CountDownTimer();

    }
  }

  private void CountDownTimer()
  {
    if (outOfBody)
    {
      CountDownTime -= Time.deltaTime;
    }
  }

  private void CheckToDespawnReaper()
  {
    if (CountDownTime <= 0 && !outOfBody)
    {
      DespawnReaper();

    }
  }

  private void CheckToSpawnReaper()
  {
    if (CountDownTime <= 0 && outOfBody)
    {
      SpawnReaper();

    }
  }

  private void DespawnReaper()
  {
    throw new NotImplementedException();
  }

  private void SpawnReaper()
  {
    throw new NotImplementedException("Spawn Reaper");

  }
}
