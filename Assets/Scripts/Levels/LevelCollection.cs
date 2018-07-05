using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelCollection
{
    [SerializeField]
    private List<LevelData> levelsData;

    public List<LevelData> LevelsData
    {
        get
        {
            return levelsData;
        }

        set
        {
            levelsData = value;
        }
    }


}
