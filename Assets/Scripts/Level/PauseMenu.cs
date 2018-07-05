using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    LevelManager LevelManager => LevelManager.Instance;

    public void Retry()
    {
        Extensions.Log(GetType(), "Retry pressed");
        LevelManager.TogglePauseMenu();
        LevelManager.SceneData.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.TogglePauseMenu();
        LevelManager.SceneData.Storage.SceneFader.FadeTo(LevelManager.SceneData.SceneNode.Parent.Data);
    }
}
