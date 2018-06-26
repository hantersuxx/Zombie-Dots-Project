using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    public static void SaveLocally<T>(T data, string filename)
    {
        string json = JsonUtility.ToJson(data);
        string fullFilename = $@"{Application.persistentDataPath}/{filename}";
        File.WriteAllText(fullFilename, json);
        Extensions.Log(typeof(T), $"Saved to: {fullFilename}");
        //BinaryFormatter bf = new BinaryFormatter();
        //using (FileStream file = File.Create(fullFilename))
        //{
        //    //bf.Serialize(file, json);
        //}
    }

    public static T LoadLocally<T>(string filename)
    {
        string fullFilename = $@"{Application.persistentDataPath}/{filename}";
        if (File.Exists(fullFilename))
        {
            Extensions.Log(typeof(T), $"Loaded from: {fullFilename}");
            return JsonUtility.FromJson<T>(File.ReadAllText(fullFilename));
            //BinaryFormatter bf = new BinaryFormatter();
            //using (FileStream file = File.Open(fullFilename, FileMode.Open))
            //{
            //    return JsonUtility.FromJson<T>(bf.Deserialize(file).ToString());
            //}
        }
        return default(T);
    }
}
