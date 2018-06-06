using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text zombiesKilled;
    [SerializeField]
    private Text humansLeft;

    public Text ZombiesKilled => zombiesKilled;
    public Text HumansLeft => humansLeft;

    private void Update()
    {
        ZombiesKilled.text = LevelManager.Instance.ZombiesKilled.ToString();
        HumansLeft.text = LevelManager.Instance.Goal.ToString();
    }

    public void Retry()
    {
        LevelManager.Instance.ToggleGameOver();
        LevelManager.Instance.SceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Go to menu from game over");
    }
}
