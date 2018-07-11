using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    LevelManager LevelManager => LevelManager.Instance;

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
        LevelManager.UpdateText(HealthPointsText, $"{LevelVariables.Instance.CurrentHealth} HP");
        LevelManager.UpdateText(ScorePointsText, $"Score: {LevelVariables.Instance.Score}");
        LevelManager.UpdateText(GoalPointsText, $"Goal: {LevelVariables.Instance.GoalValue}");
    }

    public void Pause()
    {
        LevelManager.TogglePauseMenu();
    }

    public void Restart()
    {
        LevelManager.SceneData.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
}
