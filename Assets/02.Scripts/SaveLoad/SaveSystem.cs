using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class SaveSystem
{
    public static bool SavePlayer(PlayerInformation player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSaveFile";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerSaveData data = new PlayerSaveData(player);

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
