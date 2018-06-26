using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObject
{
    [SerializeField, TagSelector]
    private string tag;
    public string Tag { get => tag; private set => tag = value; }

    [SerializeField]
    private int rate;
    public int Rate { get => rate; private set => rate = value; }

    [SerializeField]
    private CreatureStats stats;
    public CreatureStats Stats { get => (useStats) ? stats : null; private set => stats = value; }

    [SerializeField]
    private bool useStats = false;
    public bool UseStats { get => useStats; private set => useStats = value; }
}