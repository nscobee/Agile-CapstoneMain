using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

//the main level data
[Serializable]
public class LevelData
{
    public static LevelData current;

    public int sceneNum;
    public string levelName;
    public PlayerData player = new PlayerData();
    public List<EnemyData> enemyList = new List<EnemyData>();
}

//classes of the stuff in the level
[Serializable]
public class EnemyData
{
    public float xPos, yPos, zPos;
    public float currentHealth;
}

[Serializable]
public class PlayerData
{
    public float xPos, yPos, zPos;
    public float currentHealth;
    public bool isPossessing;
}


