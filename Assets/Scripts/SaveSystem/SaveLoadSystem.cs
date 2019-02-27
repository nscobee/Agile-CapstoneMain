using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveLoadSystem
{
    public static bool MakeNewPlayerSave(string saveName)
    {
        Debug.Log("into making save");
        if (!File.Exists(Application.persistentDataPath + "/" + saveName + "/Position.loc"))
        {
            Debug.Log(Application.persistentDataPath.ToString());

            SaveData newSave = ScriptableObject.CreateInstance("SaveData") as SaveData;

            Directory.CreateDirectory(Application.persistentDataPath + "/" + saveName);


            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/" + saveName + "/Position.loc", FileMode.Create);

            SerializedSaveData data = new SerializedSaveData(newSave);

            bf.Serialize(stream, data);

            stream.Close();

            return true;
        }
        else
        {
            Debug.Log("no filepath");
            return false;
            
        }
    }

    /** SavePlayer Does:
     * Gives it in SaveData and saves it into the persistent data path
     */
    public static void SavePlayer(SaveData save, string saveName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + saveName + "/SaveData.sav", FileMode.Create);

        Debug.Log(Application.persistentDataPath.ToString());

        SerializedSaveData data = new SerializedSaveData(save);

        bf.Serialize(stream, data);

        stream.Close();

    }

    /** LoadPlayer Does:
     * This checks if there is a save data and if there is it takes it and opens it as the serializedSaveData and returns it as basic save data
     */
    public static SaveData LoadPlayer(string saveName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + saveName + "/Position.loc"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + saveName + "/Position.loc", FileMode.Open);

            SerializedSaveData data = bf.Deserialize(stream) as SerializedSaveData;

            stream.Close();

            SaveData saveInfo = ScriptableObject.CreateInstance<SaveData>();

            saveInfo.sceneName = data.sceneName;
            saveInfo.checkpointNumber = data.checkpointLocation;

            return saveInfo;
        }

        return null;
    }

    /** SerializedSaveData Does:
     * This basically acts as a dummy save data but allows it to be serialize able
     */
    [Serializable]
    private class SerializedSaveData
    {
        public string sceneName;
        public int checkpointLocation;

        public SerializedSaveData(SaveData save)
        {
            sceneName = save.sceneName;
            checkpointLocation = save.checkpointNumber;

        }

        public SerializedSaveData()
        {
            throw new NotImplementedException("We need to set up a defaut save point for making a new save\nThis will probablly be the first save point");

        }
    }
}

