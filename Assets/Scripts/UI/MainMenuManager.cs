using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** MainMenuManager:
 * Made by: Brandon Laing
 * Edited by: Brandon Laing
 * 
 * Overview: This will control everything having to do with the main menu including continuing a game,
 * starting a new game, loading a game, changing options, and quitting.
 */
public class MainMenuManager : MonoBehaviour
{
  public static MainMenuManager mainMenuManager;

  #region Inspector Variables
  public GameObject mainMenuCanvas;

  [Header("New Save")]
  public GameObject newGameCanvas;
  public InputField newSceneInput;

  [Header("Load Save")]
  public GameObject loadCanvas;
  public GameObject loadPrefab;
  public LocationSaveData inspectorSaveData;
  public bool loadSaveData = false;

  [Header("Options")]
  public GameObject optionsCanvas;

  [Header("Other")]
  public OurSceneManager sceneManager;
  #endregion

  #region Canvas Transitions
  /** ResetMainMenu Does:
   * This takes each overlay canvas in the main menu and sets all of them to inactive
   * other than the main one
   */
  private void ResetMainMenu()
  {
    mainMenuCanvas.SetActive(true);
    newGameCanvas.SetActive(false);
    optionsCanvas.SetActive(false);
    loadCanvas.SetActive(false);
  }

  // when ran this swaps the Canvas UI from the main menu to make new Save
  public void _StartMakingNewSave()
  {
    mainMenuCanvas.SetActive(false);
    newGameCanvas.SetActive(true);

  }

  public void _StartLoadingSaveFromSaves()
  {
    mainMenuCanvas.SetActive(false);
    loadCanvas.SetActive(true);
    CreatePannelsForLoad();

  }

  private void CreatePannelsForLoad()
  {
    DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath + "/Saves/");

    DirectoryInfo[] fileInfo = info.GetDirectories();

    for (int i = 0; i < fileInfo.Length; i++)
    {
      GameObject fileInfoObject = Instantiate(loadPrefab, loadCanvas.transform);

      LocationSaveData saveData = SaveLoadSystem.LoadPlayerLocation(fileInfo[i].Name);

      fileInfoObject.GetComponent<SaveFileUpdater>().SetInfo(fileInfo[i].Name, saveData.sceneName, saveData.checkpointNumber.ToString());

    }

  }

  public void _GoBackToMainMenu()
  {
    mainMenuCanvas.SetActive(true);
    newGameCanvas.SetActive(false);
    optionsCanvas.SetActive(false);
    loadCanvas.SetActive(false);

  }
  #endregion

  private void Start()
  {
    mainMenuManager = this;

    sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<OurSceneManager>();

    ResetMainMenu();
    SaveLoadSystem.CheckForSaveFolder();

    Debug.Log(Application.persistentDataPath);
  }

  public void _MakeNewSave()
  {
    // try to make new player save. returns true if it makes it
    if (SaveLoadSystem.MakeNewPlayerSave(newSceneInput.text))
    {
      // makes currentSaveFile
      SaveLoadManager.currentSaveFile = newSceneInput.text;
      PlayerPrefs.SetString("LastSaveFile", SaveLoadManager.currentSaveFile);

      LocationSaveData newSave = SaveLoadSystem.LoadPlayerLocation(SaveLoadManager.currentSaveFile);

      SaveLoadManager.currentLocationFile = newSave;

      sceneManager.LoadNewSceneAdditive(newSave.sceneName);

    }
    else
    {
      Debug.LogError("Tried to overwriteSave");
    }
  }

  // This loads directly to any scene just given its name
  public void _LoadScene(string saveFileName)
  {
    LocationSaveData locLoadData = SaveLoadSystem.LoadPlayerLocation(saveFileName);

    OurSceneManager.ourSceneMangager.LoadNewSceneAdditive(locLoadData.sceneName);

  }


  private void Update()
  {
    if (loadSaveData)
    {
      loadSaveData = false;
      sceneManager.LoadNewSceneAdditive(inspectorSaveData.sceneName);
    }
  }
}

// F
// In respects to the other agile groups