using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuStorage : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader;
    public SceneFader SceneFader => sceneFader;

    [SerializeField, Scene]
    private string levelSelect;
    public string LevelSelect => levelSelect;

    [SerializeField]
    private Text currentVersion;
    public Text CurrentVersion => currentVersion;
}
