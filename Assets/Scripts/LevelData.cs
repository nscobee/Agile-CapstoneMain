using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;

//the main level data
[Serializable]
public class LevelData
{
    public static LevelData current;

    public string levelName;
    public PlayerData player = new PlayerData();
    public List<EnemyData> enemyList = new List<EnemyData>();
}

//classes of the stuff in the level
[Serializable]
public class EnemyData
{
    public Transform position;
    public float currentHealth;
    //public bool isBeingPossessed;
}

[Serializable]
public class PlayerData
{
    public Transform position;
    public float currentHealth;
    public bool isPossessing;
}


