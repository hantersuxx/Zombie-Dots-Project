using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    LevelManager LevelManager => LevelManager.Instance;
    LevelVariables LevelVariables => LevelVariables.Instance;

    [SerializeField]
    private Text zombiesKilled;
    public Text ZombiesKilled => zombiesKilled;

    [SerializeField]
    private Text hpLeft;
    public Text HpLeft => hpLeft;

    private void OnEnable()
    {
        ZombiesKilled.text = LevelVariables.Score.ToString();
        HpLeft.text = LevelVariables.CurrentHealth.ToString();
    }

    public void Continue()
    {
        Extensions.Log(GetType(), "Continue pressed");
        if (LevelsManager.Instance.NextLevelData != null)
        {
            LevelManager.ToggleCompleteLevel();
            LevelManager.SceneData.Storage.SceneFader.FadeTo(LevelsManager.Instance.NextLevelData.AssociatedScene);
        }
        else
        {
            Menu();
        }
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.ToggleCompleteLevel();
        LevelManager.SceneData.Storage.SceneFader.FadeTo(LevelManager.SceneData.SceneNode.Parent.Data);
    }
}
