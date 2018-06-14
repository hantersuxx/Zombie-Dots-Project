using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    [Scene]
    private string levelSelect;
    [SerializeField]
    [Scene]
    private string levelTest;
    [SerializeField]
    private SceneFader sceneFader;

    public string LevelSelect => levelSelect;
    public string LevelTest => levelTest;
    public SceneFader SceneFader => sceneFader;

    public void Play()
    {
        SceneFader.FadeTo(LevelTest);
    }

    public void Achievements()
    {

    }

    public void Leaderboard()
    {

    }

    public void Quit()
    {
        SceneFader.FadeTo(LevelSelect);
    }
}
