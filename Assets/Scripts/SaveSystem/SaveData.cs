using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * This is a data type that holds a scene name and checkpoint number
 */
[CreateAssetMenu]
public class SaveData : ScriptableObject
{
  public string sceneName;
  public int checkpointNumber;

  private string defaultName = "Scene01";
  private int defaultCheckpoint = 0;

  // both of these take a scene name and checkpoint number and save them to the data type
  #region Constructors
  // first constuctor takes a scene and a checkpoint number
  public SaveData(Scene scene, int checkpoint)
  {
    sceneName = scene.name;
    checkpointNumber = checkpoint;

  }

  // second takes a string and checkpoint number
  public SaveData(string name, int checkpoint)
  {
    sceneName = name;
    checkpointNumber = checkpoint;

  }

  public SaveData()
  {
    sceneName = defaultName;
    checkpointNumber = defaultCheckpoint;

  }
  #endregion

}
