using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject howToCanvas;
    public GameObject creditsCanvas;

    public SaveLoadController slControl;
    
    private PhantomControls player;

    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        howToCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void _GoBackToMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        howToCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void _MakeNewSave()
    {
        SceneManager.LoadScene(1);
    }

    public void _LoadGame()
    {
        slControl.LoadLevel();
    }

    public void _ShowHowTo()
    {
        howToCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    public void _ShowCredits()
    {
        creditsCanvas.SetActive(true);
        howToCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
    }

    public void _QuitGame()
    {
        Application.Quit();
    }

    private void OnLevelWasLoaded(int level)
    {
        player = GameObject.Find("Phantom2.0").GetComponent<PhantomControls>();
        GameObject entry = GameObject.Find("EntryPoint");
        player.transform.position = entry.transform.position;
    }

}