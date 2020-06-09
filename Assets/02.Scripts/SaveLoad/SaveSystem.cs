//
// SaveSystem BINARY SAVE AND LOAD OF PLAYER INFORMATION
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
// This static class saves and loads the player informaiton by binary serialisation and deserialisation.
// This save file is locally stored at C:\Users\[USER]\AppData\LocalLow\DefaultCompany\Invasion_Extermination
// 
// AUT University - 2020 - Kelly Luo
// 
// Revision History
// ~~~~~~~~~~~~~~~~
// 18.05.2020 Creation date
// 7.06.2020 Added error checking during save and load

//
// .NET support packages
// ~~~~~~~~~~~~~~~~~~~~~
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
//
// Unity support packages
// ~~~~~~~~~~~~~~~~~~~~~
using UnityEngine;

public static class SaveSystem
{
    //
    // SavePlayer()
    // ~~~~~~~~~~~~
    // The PlayerSaveData converts the incoming player information to be serialisable which is then
    // performed binary serialisation with the save file path.
    //
    // playerInfo   The player information wanting to be saved to the local file
    //
    // returns      True if the player information was successfully saved to the local location without disruption
    //
    public static bool SavePlayer(PlayerInformation playerInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSaveFile";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerSaveData data = new PlayerSaveData(playerInfo);

        Debug.Log($"Player was SAVED. Health:{data.Health} Money:{data.Money} " +
            $"Score:{data.Score} Position: x={data.position[0]} y={data.position[1]} y={data.position[2]}");

        try
        {
            formatter.Serialize(stream, data);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed to serialize. Reason: " + e.Message);
            return false;
        }
        finally
        {
            stream.Close();
        }

        return true;
    }

    //
    // LoadPlayer()
    // ~~~~~~~~~~~~
    // From the save file path, the file is read (if exists) and deserialised from binary and converted to
    // PlayerSaveData to be further parsed to load and update the player information.
    //
    // returns      loaded player save data from the file read
    //
    public static PlayerSaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerSaveFile";
        FileStream stream = null;

        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = new FileStream(path, FileMode.Open);

                PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;

                return data;
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to deserialize. Reason: " + e.Message);
                return null;
            }
            finally
            {
                stream.Close();
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
