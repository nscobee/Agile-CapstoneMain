using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
  public GameObject mainMenuCanvas;

  [Header("New Save")]
  public GameObject newGameCanvas;
  public InputField newSceneInput;

  [Header("Load Save")]
  public GameObject loadCanvas;

  [Header("Options")]
  public GameObject optionsCanvas;

  private void Start()
  {
    mainMenuCanvas.SetActive(true);
    newGameCanvas.SetActive(false);
    optionsCanvas.SetActive(false);
    loadCanvas.SetActive(false);

    Debug.Log(Application.persistentDataPath);

  }

  // This loads directly to any scene just given its name
  public void _LoadScene(string name)
  {
    SceneManager.LoadScene(name);

  }

  // when ran this swaps the Canvas UI from the main menu to make new Save
  public void _StartMakingNewSave()
  {
    mainMenuCanvas.SetActive(false);
    newGameCanvas.SetActive(true);

  }

  public void _GoBackToMainMenu()
  {
    mainMenuCanvas.SetActive(true);
    newGameCanvas.SetActive(false);
    optionsCanvas.SetActive(false);
    loadCanvas.SetActive(false);

  }

  public void _MakeNewSave()
  {
    if (SaveLoadSystem.MakeNewPlayerSave(newSceneInput.text))
    {
      SaveLoadSystem.LoadPlayer(newSceneInput.text);

    }

    else
    {
      Debug.LogError("Tried to overwriteSave");

    }
    //Debug.Log(newSceneInput.text);


  }
}
// F
// In respects to the other agile groups