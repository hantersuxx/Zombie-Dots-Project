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

    private void OnEnable()
    {
        Time.timeScale = 0f;
        LevelManager.Instance.LevelMenuPanel.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.LevelMenuPanel.SetActive(true);
    }

    private void Update()
    {
        ZombiesKilled.text = LevelManager.Instance.ZombiesKilled.ToString();
        HumansLeft.text = LevelManager.Instance.Goal.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Go to menu from game over");
    }
}
