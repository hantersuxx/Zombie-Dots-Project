using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField]
    private Text zombiesKilled;
    public Text ZombiesKilled => zombiesKilled;

    [SerializeField]
    private Text hpLeft;
    public Text HpLeft => hpLeft;

    private void OnEnable()
    {
        ZombiesKilled.text = LevelStats.Instance.ZombiesKilled.ToString();
        HpLeft.text = LevelStats.Instance.CurrentHealth.ToString();
    }

    public void Continue()
    {
        Debug.Log("Continue from level won");
        LevelManager.Instance.ToggleCompleteLevel();
        LevelManager.Instance.Storage.SceneFader.FadeTo("LevelSelect");
    }

    public void Menu()
    {
        Debug.Log("Go to menu from level won");
    }
}
