using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
  public static string currentSaveFile = null;

  public static LocationSaveData currentLocationFile;
  public LocationSaveData dummyCurrentLocationFile;

  private void Update()
  {
    dummyCurrentLocationFile = currentLocationFile;

  }
}
