using UnityEngine;

public class LocalData
{
    public void SetLocally<T>(T value)
    {
        Serializer.SaveLocally<T>(value, Globals.GetFilename<T>());
    }

    public T GetLocally<T>()
    {
        return Serializer.LoadLocally<T>(Globals.GetFilename<T>());
    }
}
