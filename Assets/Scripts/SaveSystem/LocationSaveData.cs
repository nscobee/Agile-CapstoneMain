using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** LocationSaveData:
 * Created by: Brandon Laing
 * Edited by: Brandon Laing
 * 
 * This is a data type that hold the name of a scene and a checkpoint location within that scene
 */
[CreateAssetMenu]  // this allows us to make this data type from the asset menu
public class LocationSaveData : ScriptableObject
{
  #region Variables
  public string sceneName;
  public int checkpointNumber;

  private string defaultSceneName = "Scene01";
  private int defaultCheckpointNumber = 0;
  #endregion

  #region Constructors
  /** BaseConstructor
   * Makes new saveLocation with default sceneName and checkpointNumber
   */
  public LocationSaveData()
  {
    sceneName = defaultSceneName;
    checkpointNumber = defaultCheckpointNumber;

  }

  /** @param: scene, @param checkpoint
   * this takes in a scene and checkpoint and makes location save data from it.
   * The checkpoint just moves into the checkpoint number and the sceneName
   * is taken from the @scene
   */
  public LocationSaveData(Scene scene, int checkpoint)
  {
    sceneName = scene.name;
    checkpointNumber = checkpoint;

  }

  /** @param: name, @param checkpoint
   * Takes name and puts it into sceneName and moves checkpoint into checkpointNumber
   */
  public LocationSaveData(string name, int checkpoint)
  {
    sceneName = name;
    checkpointNumber = checkpoint;

  }
  #endregion
}
