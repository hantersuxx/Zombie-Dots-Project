using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int levelSelectNum;
    [SerializeField]
    private int levelTestNum;
    [SerializeField]
    private SceneFader sceneFader;

    public int LevelSelectNum => levelSelectNum;
    public int LevelTestNum => levelTestNum;
    public SceneFader SceneFader => sceneFader;

    public void Play()
    {
        SceneFader.FadeTo(LevelTestNum);
    }

    public void Achievements()
    {

    }

    public void Leaderboard()
    {

    }

    public void Quit()
    {
        SceneFader.FadeTo(LevelSelectNum);
    }
}
