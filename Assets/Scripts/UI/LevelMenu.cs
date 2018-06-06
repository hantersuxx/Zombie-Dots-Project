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
        LevelManager.UpdateText(HealthPointsText, $"{LevelManager.Instance.CurrentHealth} HP");
        LevelManager.UpdateText(ScorePointsText, $"Score: {LevelManager.Instance.Score}");
        LevelManager.UpdateText(GoalPointsText, $"{LevelManager.Instance.Goal} left to save");
    }

    public void Restart()
    {
        LevelManager.Instance.SceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }
}
