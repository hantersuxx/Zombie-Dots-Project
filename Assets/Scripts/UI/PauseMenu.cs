using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : BackNavigation
{
    public void Retry()
    {
        Extensions.Log(GetType(), "Retry pressed");
        LevelManager.Instance.TogglePauseMenu();
        LevelManager.Instance.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.Instance.TogglePauseMenu();
        NavigateBack(LevelManager.Instance.Storage.SceneFader);
    }
}
