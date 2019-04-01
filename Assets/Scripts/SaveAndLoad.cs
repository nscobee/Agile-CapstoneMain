using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveAndLoad
{
    public static List<LevelData> savedGames = new List<LevelData>();
    public static string saveNameFolder = "ThisIsATest";

    public static void Save()
    {
        savedGames.Add(LevelData.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + saveNameFolder + "/savedGames.txt");
        bf.Serialize(file, SaveAndLoad.savedGames);
        file.Close();
    }

    public static void Load(string levelNameFolder)
    {
        if(File.Exists(Application.persistentDataPath +"/" + levelNameFolder + "/savedGames.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + levelNameFolder + "/savedGames.txt", FileMode.Open);
            SaveAndLoad.savedGames = (List<LevelData>)bf.Deserialize(file);
            file.Close();
        }
    }
}
