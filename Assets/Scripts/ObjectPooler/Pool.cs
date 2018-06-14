using UnityEngine;

[System.Serializable]
public class Pool
{
    [SerializeField, TagSelector]
    private string tag;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int size;

    public string Tag
    {
        get
        {
            return tag;
        }
        set
        {
            tag = value;
        }
    }

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
        }
    }

    public int Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
        }
    }
}
