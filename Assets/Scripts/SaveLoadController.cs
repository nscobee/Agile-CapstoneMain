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
        playerDat.position = player.gameObject.transform;
        playerDat.isPossessing = player.isPossessing;
        ld.player = playerDat;
        
        foreach (GameObject meleeEnemy in GameObject.FindGameObjectsWithTag("Melee"))
        {
            EnemyData enemyDat = new EnemyData();
            enemyDat.position = meleeEnemy.transform;
            enemyDat.currentHealth = meleeEnemy.gameObject.GetComponent<AIHealth>().currentHealth;
            ld.enemyList.Add(enemyDat);
        }

        foreach (GameObject mageEnemy in GameObject.FindGameObjectsWithTag("mage"))
        {
            EnemyData enemyDat = new EnemyData();
            enemyDat.position = mageEnemy.transform;
            enemyDat.currentHealth = mageEnemy.gameObject.GetComponent<AIHealth>().currentHealth;
            ld.enemyList.Add(enemyDat);
        }

    }
    
}
