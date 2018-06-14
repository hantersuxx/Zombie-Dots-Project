using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObject
{
    [SerializeField, TagSelector]
    private string tag;
    [SerializeField]
    private int rate;

    public string Tag
    {
        get
        {
            return tag;
        }
        private set
        {
            tag = value;
        }
    }

    public int Rate
    {
        get
        {
            return rate;
        }

        private set
        {
            rate = value;
        }
    }
}