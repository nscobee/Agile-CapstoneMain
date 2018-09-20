using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This lets the developers to move from scene to scene using premade save files
 * all we have to do is put in a savefile then press the load button and it should
 * load the new scene and place the player at the desired position in the level
 */
public class SaveLoadDebuging : MonoBehaviour
{
  public LocationSaveData loadFile;

  public bool loadButton;

  public OurSceneManager sceneMangment;

  private void Update()
  {
    if (loadButton && loadFile != null)
    {
      loadButton = false;

      // mimics what will actually be our save feture from main menu
      sceneMangment.LoadFromSaveNew(loadFile.sceneName, loadFile.checkpointNumber);

    }

  }
}
