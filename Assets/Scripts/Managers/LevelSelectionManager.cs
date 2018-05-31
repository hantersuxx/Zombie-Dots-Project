using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField]
    private List<Level> levels = new List<Level>();

    public List<Level> Levels => levels;
}
