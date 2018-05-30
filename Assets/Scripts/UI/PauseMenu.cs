using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused { get; set; } = false;
    [SerializeField]
    private GameObject pauseMenuPanel;
    public GameObject PauseMenuPanel => pauseMenuPanel;

    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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
