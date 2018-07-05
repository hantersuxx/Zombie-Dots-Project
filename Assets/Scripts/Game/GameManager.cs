using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStats
{
    public SceneData<MainMenuStorage> SceneData { get; private set; } = new SceneData<MainMenuStorage>();

    [SerializeField]
    private GameData gameData;
    public GameData GameData { get => gameData; private set => gameData = value; }

    public GooglePlayServices GooglePlayServices { get; private set; }
    public FirebaseServices FirebaseServices { get; private set; }

    public static GameManager Instance { get; private set; } = null;

    private bool DataFetched { get; set; } = false;
    private MNPopup MNPopup { get; set; } = null;
    private bool DataFetchedPreloaderShowed { get; set; } = false;
    private bool ConnectionPreloaderShowed { get; set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            SetupServices();
            Subscribe();
            StartCoroutine(WaitForDataFetched());
            StartCoroutine(WaitForConnection());
            Extensions.Log(GetType(), "Instance created");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Extensions.Log(GetType(), "Instance exists");
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Subscribe()
    {
        GooglePlayServices.Authenticate += GooglePlayServicesAuthenticated;
        GooglePlayServices.SavedGameRead += GooglePlayServicesSavedGameRead;
    }

    private void GooglePlayServicesAuthenticated(object sender, AuthenticateEventArgs e)
    {
        if (e.Authenticated)
        {
            GooglePlayServices.Read();
        }
        else
        {
            MNPopup.Show();
        }
    }

    private void GooglePlayServicesSavedGameRead(object sender, SavedGameEventArgs e)
    {
        DataFetched = true;
        if (e.Data == null)
        {
            HandleFirstLoad();
        }
        else
        {
            HandleExists(e.Data);
        }
        StartCoroutine(AutoSave(15f));
    }

    private void Unsubsribe()
    {
        GooglePlayServices.Authenticate -= GooglePlayServicesAuthenticated;
        GooglePlayServices.SavedGameRead -= GooglePlayServicesSavedGameRead;
    }

    private void SetupServices()
    {
        GooglePlayServices = new GooglePlayServices();
        FirebaseServices = new FirebaseServices();
        SceneData.SceneNode = new TreeNode<string>(SceneManager.GetActiveScene().name);
        MNPopup = CreateDialog();
        GooglePlayServices.SignIn();
    }

    private MNPopup CreateDialog()
    {
        MNPopup dialog = new MNPopup("Warning!", "Sign in to Google Play Games in order to continue.");
        dialog.AddAction("Got it!", () => { });
        return dialog;
    }

    private IEnumerator AutoSave(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            GooglePlayServices.Write(GameData);
        }
    }

    private IEnumerator WaitForDataFetched()
    {
        while (true)
        {
            if (!DataFetched && !DataFetchedPreloaderShowed)
            {
                MNP.ShowPreloader("Loading data", "Please wait");
                DataFetchedPreloaderShowed = true;
            }
            else if (DataFetched && DataFetchedPreloaderShowed)
            {
                MNP.HidePreloader();
                DataFetchedPreloaderShowed = false;
                break;
            }

            yield return null;
        }
    }

    private IEnumerator WaitForConnection()
    {
        while (true)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable && !ConnectionPreloaderShowed)
            {
                MNP.ShowPreloader("", "Waiting for internet connection. Turn on mobile data or Wi-Fi.");
                ConnectionPreloaderShowed = true;
            }
            else if ((Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork
                || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                && ConnectionPreloaderShowed)
            {
                MNP.HidePreloader();
                ConnectionPreloaderShowed = false;
            }

            yield return null;
        }
    }

    private void HandleFirstLoad()
    {
        for (int i = 0; i < GameData.LevelCollection.LevelsData.Count; i++)
        {
            if (i == 0)
            {
                GameData.LevelCollection.LevelsData[i].Level = Level.CreateUnlocked();
            }
            else
            {
                GameData.LevelCollection.LevelsData[i].Level = Level.CreateLocked();
            }
        }
    }

    private void HandleExists(GameData retrievedData)
    {
        if (retrievedData.LevelCollection.LevelsData.Count != GameData.LevelCollection.LevelsData.Count)
        {
            retrievedData.LevelCollection = Merge(retrievedData.LevelCollection);
        }
        GameData.LevelCollection = retrievedData.LevelCollection;
        GameData.GameStats = retrievedData.GameStats;
    }

    private LevelCollection Merge(LevelCollection retrievedData)
    {
        var addable = GameData.LevelCollection.LevelsData.Skip(retrievedData.LevelsData.Count)
                        .Select(d => new LevelData { AssociatedScene = d.AssociatedScene, Level = Level.CreateLocked() });
        if (retrievedData.LevelsData.Last().Level.Completed)
        {
            addable.ElementAt(0).Level = Level.CreateUnlocked();
        }
        retrievedData.LevelsData.AddRange(addable);
        return retrievedData;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Extensions.Log(GetType(), "Focus lost");
            InitializeStars();
            InitializeScore();
        }
    }

    public void InitializeScore()
    {
        GameData.GameStats.InitializeScore();
        GooglePlayServices.InitializeScore();
    }

    public void InitializeStars()
    {
        GameData.GameStats.InitializeStars();
        GooglePlayServices.InitializeStars();
    }

    public void IncreaseZombiesKilled()
    {
        GameData.GameStats.IncreaseZombiesKilled();
        GooglePlayServices.IncreaseZombiesKilled();
    }

    public void IncreaseHumansKilled()
    {
        GameData.GameStats.IncreaseHumansKilled();
    }

    public void IncreaseHumansSaved()
    {
        GameData.GameStats.IncreaseHumansSaved();
        GooglePlayServices.IncreaseHumansSaved();
    }
}