using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveAndLoad
{
    public static List<LevelData> savedGames = new List<LevelData>();
    //public static LevelData savedGame = new LevelData();
    public static string saveNameFolder = "ThisIsATest";
    
    public static void Save()
    {
        UnityEngine.Debug.Log("Saving from SaveAndLoad");
        savedGames.Add(LevelData.current);
        BinaryFormatter bf = new BinaryFormatter();
        var directory = Directory.CreateDirectory(Application.persistentDataPath + "/REVENANTSOUL");
        FileStream file = File.Create(Application.persistentDataPath + "/" + directory + "/savedGames.txt");
        bf.Serialize(file, SaveAndLoad.savedGames);
        file.Close();
    }

    public static LevelData Load()
    {
        if(File.Exists(Application.persistentDataPath + "/" + saveNameFolder + "/savedGames.txt"))
        {
            UnityEngine.Debug.Log("Loading from file: " + Application.persistentDataPath + "/" + saveNameFolder + "/savedGames.txt");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveNameFolder + "/savedGames.txt", FileMode.Open);
            SaveAndLoad.savedGames = (List<LevelData>)bf.Deserialize(file);
            file.Close();
        }
        return (SaveAndLoad.savedGames[0]);
    }
}
