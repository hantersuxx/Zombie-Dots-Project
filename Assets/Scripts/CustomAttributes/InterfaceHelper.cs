using UnityEngine;

[System.Serializable]
public class InterfaceHelper
{
    public Component target;

    public T GetInterface<T>() where T : class
    {
        return target as T;
    }
}