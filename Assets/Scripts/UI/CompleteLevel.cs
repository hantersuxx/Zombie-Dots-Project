using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : BackNavigation
{
    [SerializeField]
    private Text zombiesKilled;
    public Text ZombiesKilled => zombiesKilled;

    [SerializeField]
    private Text hpLeft;
    public Text HpLeft => hpLeft;

    private void OnEnable()
    {
        ZombiesKilled.text = LevelVariables.Instance.Score.ToString();
        HpLeft.text = LevelVariables.Instance.CurrentHealth.ToString();
    }

    public void Continue()
    {
        Extensions.Log(GetType(), "Continue pressed");
        if (LevelsManager.Instance.NextLevelData != null)
        {
            LevelManager.Instance.ToggleCompleteLevel();
            LevelManager.Instance.Storage.SceneFader.FadeTo(LevelsManager.Instance.NextLevelData.AssociatedScene);
        }
        else
        {
            Menu();
        }
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.Instance.ToggleCompleteLevel();
        NavigateBack(LevelManager.Instance.Storage.SceneFader);
    }
}
