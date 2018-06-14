using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
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

    public void Retry()
    {
        LevelManager.Instance.ToggleGameOver();
        LevelManager.Instance.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Debug.Log("Go to menu from game over");
    }
}
