using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStats
{
    public MainMenuStorage Storage => FindObjectOfType<MainMenuStorage>();

    [SerializeField]
    private LevelCollection levelCollection;
    public LevelCollection LevelCollection { get => levelCollection; private set => levelCollection = value; }

    [SerializeField]
    private GameStats gameStats;
    public GameStats GameStats { get => gameStats; private set => gameStats = value; }

    [SerializeField, ReadOnly]
    private List<string> scenesNesting = new List<string>();
    public List<string> ScenesNesting { get => scenesNesting; private set => scenesNesting = value; }

    public LocalData LocalData { get; private set; }
    public GooglePlayServices GooglePlayServices { get; private set; }
    public FirebaseServices FirebaseServices { get; private set; }

    public static GameManager Instance { get; private set; } = null;

    public event EventHandler ApplicationFocusLost;

    protected virtual void OnApplicationFocusLost(EventArgs e)
    {
        ApplicationFocusLost?.Invoke(this, e);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitializeServices();
        Subscribe();
        LoadLevelCollection();
        LoadGameStats();
    }

    private void InitializeServices()
    {
        LocalData = new LocalData();
        GooglePlayServices = new GooglePlayServices();
        FirebaseServices = new FirebaseServices();
    }

    private void Subscribe()
    {
        SceneManager.sceneUnloaded += HandleSceneUnloaded;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene loadingScene, LoadSceneMode mode)
    {
        AddScene(loadingScene);
        Extensions.Log(GetType(), $"Scene \"{loadingScene.name}\" was loaded");
    }

    public void AddScene(Scene scene)
    {
        bool exists = ScenesNesting.Any(s => s == scene.name);
        if (exists)
        {
            while (scene.name != ScenesNesting.Last())
            {
                ScenesNesting.Remove(ScenesNesting.Last());
            }
        }
        else
        {
            ScenesNesting.Add(scene.name);
        }
    }

    public string GetPreviousScene()
    {
        try
        {
            int i = ScenesNesting.Count - 2;
            if (i >= 0)
            {
                return ScenesNesting.ElementAt(ScenesNesting.Count - 2);
            }
            throw new IndexOutOfRangeException();
        }
        catch (IndexOutOfRangeException exc)
        {
            return string.Empty;
        }
    }

    private void HandleSceneUnloaded(Scene unloadingScene)
    {
        Extensions.Log(GetType(), $"Scene \"{unloadingScene.name}\" was unloaded");
    }

    private void Unsubscribe()
    {
        SceneManager.sceneUnloaded -= HandleSceneUnloaded;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void Start()
    {
        GooglePlayServices.SignIn();
    }

    private void OnDestroy()
    {
        Unsubscribe();
        GooglePlayServices.SignOut();
    }

    private void LoadLevelCollection()
    {
        InitializeDataLocally<LevelCollection>(HandleFirstLevelCollectionLoad, HandleLevelCollectionExists);
    }

    private void LoadGameStats()
    {
        InitializeDataLocally<GameStats>(handleDataExists: HandleGameStatsExists);
    }

    private void InitializeDataLocally<T>(System.Action handleFirstLoad = null, System.Action<T> handleDataExists = null)
    {
        var localData = LocalData.GetLocally<T>();
        if (localData.IsNull())
        {
            handleFirstLoad?.Invoke();
        }
        else
        {
            handleDataExists?.Invoke(localData);
        }
    }

    private void HandleFirstLevelCollectionLoad()
    {
        for (int i = 0; i < LevelCollection.LevelsData.Count; i++)
        {
            if (i == 0)
            {
                LevelCollection.LevelsData[i].Level = Level.CreateUnlocked();
            }
            else
            {
                LevelCollection.LevelsData[i].Level = Level.CreateLocked();
            }
        }
    }

    private void HandleLevelCollectionExists(LevelCollection localData)
    {
        if (localData.LevelsData.Count != LevelCollection.LevelsData.Count)
        {
            localData = MergeLocal(localData);
        }
        LevelCollection = localData;
    }

    private void HandleGameStatsExists(GameStats gameStats)
    {
        GameStats = gameStats;
    }

    private LevelCollection MergeLocal(LevelCollection localData)
    {
        var addable = LevelCollection.LevelsData.Skip(localData.LevelsData.Count)
                        .Select(d => new LevelData { AssociatedScene = d.AssociatedScene, Level = Level.CreateLocked() });
        if (localData.LevelsData.Last().Level.Completed)
        {
            addable.ElementAt(0).Level = Level.CreateUnlocked();
        }
        localData.LevelsData.AddRange(addable);
        return localData;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            OnApplicationFocusLost(EventArgs.Empty);
            InitializeStars();
            InitializeScore();
            LocalData.SetLocally(LevelCollection);
            LocalData.SetLocally(GameStats);
        }
    }

    public void InitializeScore()
    {
        GameStats.InitializeScore();
        GooglePlayServices.InitializeScore();
    }

    public void InitializeStars()
    {
        GameStats.InitializeStars();
        GooglePlayServices.InitializeStars();
    }

    public void IncreaseZombiesKilled()
    {
        GameStats.IncreaseZombiesKilled();
        GooglePlayServices.IncreaseZombiesKilled();
    }

    public void IncreaseHumansKilled()
    {
        GameStats.IncreaseHumansKilled();
    }

    public void IncreaseHumansSaved()
    {
        GameStats.IncreaseHumansSaved();
        GooglePlayServices.IncreaseHumansSaved();
    }

    private void OpenSavedGame(string filename, Action<SavedGameRequestStatus, ISavedGameMetadata> onSavedGameOpened)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(
            filename,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            onSavedGameOpened);
    }

    private void SaveGame(ISavedGameMetadata game, byte[] savedData, Action<SavedGameRequestStatus, ISavedGameMetadata> onSavedGameWritten)
    {
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
            .WithUpdatedPlayedTime(TimeSpan.FromMinutes(game.TotalTimePlayed.Minutes + 1))
            .WithUpdatedDescription("Saved at: " + System.DateTime.Now);

        // You can add an image to saved game data (such as as screenshot)
        // byte[] pngData = <PNG AS BYTES>;
        // builder = builder.WithUpdatedPngCoverImage(pngData);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, onSavedGameWritten);
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // writing
            if (GooglePlayServices.Saving)
            {

            }
            // reading
            else
            {

            }
        }
        else
        {
            // handle error
        }
    }
}