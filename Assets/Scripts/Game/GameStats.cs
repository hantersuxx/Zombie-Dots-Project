using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameStats : IStats
{
    [SerializeField, ReadOnly]
    private int zombiesKilledTotal = 0;
    public int ZombiesKilledTotal { get => zombiesKilledTotal; private set => zombiesKilledTotal = value; }

    [SerializeField, ReadOnly]
    private int humansKilledTotal = 0;
    public int HumansKilledTotal { get => humansKilledTotal; private set => humansKilledTotal = value; }

    [SerializeField, ReadOnly]
    private int humansSavedTotal = 0;
    public int HumansSavedTotal { get => humansSavedTotal; private set => humansSavedTotal = value; }

    [SerializeField, ReadOnly]
    private int starsTotal = 0;
    public int StarsTotal { get => starsTotal; private set => starsTotal = value; }

    [SerializeField, ReadOnly]
    private int scoreTotal = 0;
    public int ScoreTotal { get => scoreTotal; private set => scoreTotal = value; }

    [SerializeField, ReadOnly]
    private bool adsEnabled = true;
    public bool AdsEnabled { get => adsEnabled; private set => adsEnabled = value; }

    public void DisableAds()
    {
        AdsEnabled = false;
    }

    public void IncreaseZombiesKilled()
    {
        ZombiesKilledTotal++;
    }

    public void IncreaseHumansKilled()
    {
        HumansKilledTotal++;
    }

    public void IncreaseHumansSaved()
    {
        HumansSavedTotal++;
    }

    public void InitializeStars()
    {
        StarsTotal = GameManager.Instance.GameData.LevelCollection.LevelsData.Sum(l => l.Level.Stars);
    }

    public void InitializeScore()
    {
        ScoreTotal = GameManager.Instance.GameData.LevelCollection.LevelsData.Sum(l => l.Level.Score);
    }
}
