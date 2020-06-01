using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerInformation player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerSaveFile";

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerSaveData data = new PlayerSaveData(player);

        Debug.Log($"Player was SAVED. Health:{data.Health} Level:{data.Level} Money:{data.Money}" +
            $"Score:{data.Score} Position: x={data.position[0]} y={data.position[1]} y={data.position[2]}");

        formatter.Serialize(stream, data);
        stream.Close();

        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/playerDataText.txt");
        sw.WriteLine(data.position[0]);
        sw.WriteLine(data.position[1]);
        sw.WriteLine(data.position[2]);
        sw.Close();
    }

    public static PlayerSaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerSaveFile";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
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
