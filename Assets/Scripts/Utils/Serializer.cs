using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static partial class Extensions
{
    public static byte[] Serialize<T>(this T value)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, value);
            return ms.ToArray();
        }
    }

    public static T Deserialize<T>(this byte[] array)
    {
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(array, 0, array.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            if (memStream.Length == 0)
            {
                return default(T);
            }
            return (T)binForm.Deserialize(memStream);
        }
    }

    public static string SerializeJson<T>(this T value)
    {
        return JsonUtility.ToJson(value);
    }

    public static T DeserializeJson<T>(this string value)
    {
        return JsonUtility.FromJson<T>(value);
    }

    public static void WriteText(string filename, string text)
    {
        string filePath = $@"{Application.persistentDataPath}/{filename}";
        File.WriteAllText(filePath, text);
        //BinaryFormatter bf = new BinaryFormatter();
        //using (FileStream file = File.Create(fullFilename))
        //{
        //    //bf.Serialize(file, json);
        //}
    }

    public static string ReadText(string filename)
    {
        string filePath = $@"{Application.persistentDataPath}/{filename}";
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
            //BinaryFormatter bf = new BinaryFormatter();
            //using (FileStream file = File.Open(fullFilename, FileMode.Open))
            //{
            //    return JsonUtility.FromJson<T>(bf.Deserialize(file).ToString());
            //}
        }
        return string.Empty;
    }
}
