using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerTranslate player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSaveFiles";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerStats data = new PlayerStats(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerStats LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerSaveFiles";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStats data = formatter.Deserialize(stream) as PlayerStats;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
