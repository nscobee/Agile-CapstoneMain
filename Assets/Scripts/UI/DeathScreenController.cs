using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void _RestartLevel()
    {
        if(SaveAndLoad.savedGames.Count > 0)
            SaveLoadController.control.LoadLevel();
    }

    public void _GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
