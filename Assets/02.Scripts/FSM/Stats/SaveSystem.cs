using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerStateController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSaveFile";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerStats data = new PlayerStats(player);

        formatter.Serialize(stream, data);
        stream.Close();

        //StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/playerDataText.txt");
        //sw.WriteLine(data.position[0]);
        //sw.WriteLine(data.position[1]);
        //sw.WriteLine(data.position[2]);
        //sw.Close();
    }

    public static PlayerStats LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerSaveFile";
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
