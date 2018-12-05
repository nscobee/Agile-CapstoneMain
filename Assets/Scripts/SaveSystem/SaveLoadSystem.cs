using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/** Save Load System:
 * Made by: Brandon Laing
 * Editied by: Brandon Laing
 * 
 * Overview: This will will hold all the information on making, saving, and loading files.
 */
public static class SaveLoadSystem
{
  /** CheckForSaveFolder Does: 
   * This will check if the folder for that contains all the saves is present and if it isnt 
   * it creates the saves folder
   */
  public static void CheckForSaveFolder()
  {
    // checks if there isnt a data path for the saves
    if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
    {
      // makes data path for saves and hides it
      Directory.CreateDirectory(Application.persistentDataPath + "/Saves").Attributes |= FileAttributes.Hidden;
    }

  }

  /** MakeNewPlayerSave Does:
   * This will make a new save folder within the saves folder with a particular saveFileName.
   * It will place the .loc at the default first level and make a new base stats file.
   *
   */
  public static bool MakeNewPlayerSave(string saveFileName)
  {
    // if there isnt already a file in the saves folder with the save name
    if (!Directory.Exists(Application.persistentDataPath + "/Saves/" + saveFileName))
    {
      // make a new location save data
      LocationSaveData newLocationSave = ScriptableObject.CreateInstance("LocationSaveData") as LocationSaveData;
      // create the location file
      Directory.CreateDirectory(Application.persistentDataPath + "/Saves/" + saveFileName);

      // open a new binary formatter
      BinaryFormatter bf = new BinaryFormatter();
      // create fileStream for the position
      FileStream stream = new FileStream(Application.persistentDataPath + "/Saves/" + saveFileName + "/Position.loc", FileMode.Create);
      // makes new serializedSaveData based on the locationSave
      SerializedLocationSaveData data = new SerializedLocationSaveData(newLocationSave);

      // puts the binary data at the stream location
      bf.Serialize(stream, data);
      // closes stream
      stream.Close();

      // returns true if it made new file
      return true;
    }

    // retuns false if it couldnt make new file
    return false;
  }

  /** SavePlayer Does:
   * Saves player info into save file
   * 
   * @param saveFileName - Takes current save file name so the system knows where to save
   * @param locSaveData - takes in a locationSaveData that hold the info to put into the .loc folder
   */
  public static void SavePlayer(string saveFileName, LocationSaveData locSaveData)
  {
    try
    {
      // opens new binary formatter
      BinaryFormatter bf = new BinaryFormatter();
      // opens new stream to locationSave file location
      FileStream locationStream = new FileStream(Application.persistentDataPath + "/Saves/" + saveFileName + "/SaveData.sav", FileMode.Append);

      // serializes the save data
      SerializedLocationSaveData locData = new SerializedLocationSaveData(locSaveData);
      // places that save data into the location
      bf.Serialize(locationStream, locData);

      // close file
      locationStream.Close();
    }
    catch
    {
      Debug.LogError("Error trying to save info into " + saveFileName);
    }
  }

  /** LoadPlayer Does:
   * this will take in a saveFileName and return a location saveData
   * @param saveFileName - Name of save folder to grab from
   */
  public static LocationSaveData LoadPlayerLocation(string saveFileName)
  {
    // check if the file location exist
    if (Directory.Exists(Application.persistentDataPath + "/Saves/" + saveFileName))
    {
      try
      {
        // open the stream and binary formatter
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/Saves/" + saveFileName + "/Position.loc", FileMode.Open);

        // deserialize the location as a serializedLocationSaveData
        SerializedLocationSaveData data = bf.Deserialize(stream) as SerializedLocationSaveData;
        // close the stream
        stream.Close();

        // make a new empty save info to hold the location save data
        LocationSaveData saveInfo = ScriptableObject.CreateInstance<LocationSaveData>();

        // set scene name and save infor from serialized into the nonserialized
        saveInfo.sceneName = data.sceneName;
        saveInfo.checkpointNumber = data.checkpointLocation;

        return saveInfo;
      }
      catch (Exception ex)
      {
        Debug.LogError("Error Loading Player Location");
        Debug.LogError(ex);
        return null;
      }
    }
    else
    {
      Debug.LogError("Tried to load save where the file location did not exist");
      return null;
    }
  }

  /** SerializedSaveData Does:
   * This basically acts as a dummy Location save data but allows it to be serialize able
   */
  [Serializable]
  private class SerializedLocationSaveData
  {
    public string sceneName;
    public int checkpointLocation;

    public SerializedLocationSaveData(LocationSaveData save)
    {
      sceneName = save.sceneName;
      checkpointLocation = save.checkpointNumber;

    }
  }
}

