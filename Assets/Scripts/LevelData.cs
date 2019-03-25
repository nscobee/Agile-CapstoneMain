using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

//the main level data
[Serializable]
public class LevelData
{
    public string levelName;
    public List<EnemyData> enemyList = new List<EnemyData>();
    private static string saveDirectory = Directory.GetCurrentDirectory() + "\\Levels\\";

    #region get/set for level data itself
    public string GetLevelName()
    {
        return levelName;
    }

    public void SetLevelName(string levelName)
    {
        this.levelName = levelName;
    }
    #endregion

    #region save/load funtionality
    public static LevelData LoadFromFile(string fileName)
    {
        string newPath = System.IO.Path.Combine(saveDirectory, fileName);

        return JsonUtility.FromJson<LevelData>(System.IO.File.ReadAllText(newPath));
    }

    public void SaveToFile(string fileName)
    {
        string newPath = System.IO.Path.Combine(saveDirectory, fileName);

        System.IO.File.WriteAllText(newPath, JsonUtility.ToJson(this, true));
    }

    #endregion
}

//classes of the stuff in the level
[Serializable]
public class EnemyData
{
    public Transform position;
    public float currentHealth;
    public bool isBeingPossessed;
}


