using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0f;
        LevelManager.Instance.PauseMenuPanel.SetActive(true);
        LevelManager.Instance.LevelMenuPanel.SetActive(false);
        LevelManager.Instance.GameIsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.PauseMenuPanel.SetActive(false);
        LevelManager.Instance.LevelMenuPanel.SetActive(true);
        LevelManager.Instance.GameIsPaused = false;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
