using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : BackNavigation
{
    [SerializeField]
    private Text zombiesKilled;
    public Text ZombiesKilled => zombiesKilled;

    [SerializeField]
    private Text hpLeft;
    public Text HpLeft => hpLeft;

    private void OnEnable()
    {
        ZombiesKilled.text = LevelVariables.Instance.Score.ToString();
        HpLeft.text = LevelVariables.Instance.CurrentHealth.ToString();
    }

    public void Retry()
    {
        Extensions.Log(GetType(), "Retry pressed");
        LevelManager.Instance.ToggleGameOver();
        LevelManager.Instance.Storage.SceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Extensions.Log(GetType(), "Menu pressed");
        LevelManager.Instance.ToggleGameOver();
        NavigateBack(LevelManager.Instance.Storage.SceneFader);
    }
}
