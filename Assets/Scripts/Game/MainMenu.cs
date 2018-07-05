using UnityEngine;

public class MainMenu : MonoBehaviour
{
    GameManager GameManager => GameManager.Instance;

    private void Start()
    {
        GameManager.SceneData.Storage.CurrentVersion.text = $"BUILD {Application.version}";
        GameManager.GooglePlayServices.SignIn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Extensions.Log(GetType(), "Quit pressed");
            Application.Quit();
        }
    }

    public void Play()
    {
        if (GameManager.GooglePlayServices.IsAuthenticated)
        {
            Extensions.Log(GetType(), "Play pressed");
            GameManager.SceneData.Storage.SceneFader.FadeTo(GameManager.SceneData.Storage.LevelSelect);
        }
        else
        {
            GameManager.GooglePlayServices.SignIn();
        }
    }

    public void Achievements()
    {
        if (GameManager.GooglePlayServices.IsAuthenticated)
        {
            Extensions.Log(GetType(), "Achievements pressed");
            GameManager.GooglePlayServices.OnAchievementsClick();
        }
        else
        {
            GameManager.GooglePlayServices.SignIn();
        }
    }

    public void Leaderboard()
    {
        if (GameManager.GooglePlayServices.IsAuthenticated)
        {
            Extensions.Log(GetType(), "Leaderboard pressed");
            GameManager.GooglePlayServices.OnLeaderboardClick();
        }
        else
        {
            GameManager.GooglePlayServices.SignIn();
        }
    }
}
