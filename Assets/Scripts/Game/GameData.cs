using System;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [SerializeField]
    private LevelCollection levelCollection;
    public LevelCollection LevelCollection { get => levelCollection; set => levelCollection = value; }

    [SerializeField]
    private GameStats gameStats;
    public GameStats GameStats { get => gameStats; set => gameStats = value; }
}