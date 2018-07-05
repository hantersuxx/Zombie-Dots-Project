using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
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

    public void Retry()
    {
        Extensions.Log(GetType(), "Retry pressed");
        LevelManager.ToggleGameOver();
        LevelManager.SceneData.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.ToggleGameOver();
        LevelManager.SceneData.Storage.SceneFader.FadeTo(LevelManager.SceneData.SceneNode.Parent.Data);
    }
}
