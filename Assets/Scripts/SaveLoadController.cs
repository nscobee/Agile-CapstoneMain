using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public static SaveLoadController control;

    private LevelData levelData = new LevelData();

    public PhantomControls player;
    public playerHealth playerHealthScript;

    void Awake()
    {
        if(control == null)
        {
            DontDestroyOnLoad(this.gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void SaveLevel(string levelName)
    {
        LevelData ld = new LevelData();

        PlayerData playerDat = new PlayerData();
        
        playerDat.currentHealth = playerHealthScript.currentHealth;
        ld.player = playerDat;
        
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Melee"))
        {
           
        }

    }
    
}
