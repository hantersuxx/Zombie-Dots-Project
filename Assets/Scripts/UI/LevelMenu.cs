using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField]
    private Text healthPointsText;
    [SerializeField]
    private Text scorePointsText;
    [SerializeField]
    private Text goalPointsText;

    public Text HealthPointsText => healthPointsText;
    public Text ScorePointsText => scorePointsText;
    public Text GoalPointsText => goalPointsText;

    private void Update()
    {
        LevelManager.UpdateText(HealthPointsText, $"{LevelStats.Instance.CurrentHealth} HP");
        LevelManager.UpdateText(ScorePointsText, $"Score: {LevelStats.Instance.Score}");
        LevelManager.UpdateText(GoalPointsText, $"Goal: {LevelStats.Instance.GoalValue}");
    }

    public void Restart()
    {
        LevelManager.Instance.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
