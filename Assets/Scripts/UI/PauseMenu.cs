using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Retry()
    {
        LevelManager.Instance.TogglePauseMenu();
        LevelManager.Instance.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Debug.Log("Loading menu...");
    }
}
