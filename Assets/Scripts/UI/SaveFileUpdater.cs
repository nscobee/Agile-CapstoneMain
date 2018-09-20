using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileUpdater : MonoBehaviour
{
  public Text fileNameUI;
  public Text sceneNameUI;
  public Text loadPointUI;

  public string fileName;
  public string sceneName;
  public string loadPoint;

  public void UpdateInfo()
  {
    fileNameUI.text = fileName;
    sceneNameUI.text = sceneName;
    loadPointUI.text = loadPoint;
  }
  public void SetInfo(string fileName, string sceneName, string loadPoint)
  {
    this.fileName = fileName;
    this.sceneName = sceneName;
    this.loadPoint = loadPoint;

    UpdateInfo();
  }

  public void _LoadClickedScene()
  {
    Debug.Log("SHIT");

    MainMenuManager.mainMenuManager._LoadScene(fileName);

  }
}
