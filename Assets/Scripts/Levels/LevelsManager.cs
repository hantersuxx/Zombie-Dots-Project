using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public SceneData<LevelsStorage> SceneData { get; private set; } = new SceneData<LevelsStorage>();

    public LevelCollection LevelCollection => GameManager.Instance.GameData.LevelCollection;

    public Dictionary<LevelData, GameObject> LevelDataViews { get; private set; } = new Dictionary<LevelData, GameObject>();

    [SerializeField, ReadOnly]
    private LevelData currentLevelData;
    public LevelData CurrentLevelData { get => currentLevelData; private set => currentLevelData = value; }

    [SerializeField, ReadOnly]
    private LevelData nextLevelData;
    public LevelData NextLevelData
    {
        get
        {
            nextLevelData = null;
            int currentIndex = LevelCollection.LevelsData.IndexOf(CurrentLevelData);
            if (currentIndex >= 0 && currentIndex + 1 < LevelCollection.LevelsData.Count)
            {
                nextLevelData = LevelCollection.LevelsData.ElementAt(currentIndex + 1);
            }
            return nextLevelData;
        }
    }

    public static LevelsManager Instance { get; private set; } = null;

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
    }

    private void Start()
    {
        SceneData.SceneNode = GameManager.Instance.SceneData.SceneNode.AddChild(SceneManager.GetActiveScene().name);
    }

    public void InitializeContentView()
    {
        LevelDataViews.Clear();
        for (int i = 0; i < LevelCollection.LevelsData.Count; i++)
        {
            GameObject view = null;
            if (LevelCollection.LevelsData[i].Level.Locked)
            {
                view = AddLockedItemToView();
            }
            else
            {
                view = AddUnlockedItemToView(LevelCollection.LevelsData[i]);
                view.GetComponentInChildren<Text>().text = (i + 1).ToString();
            }
            LevelDataViews.Add(LevelCollection.LevelsData[i], view);
        }
    }

    public void SelectLastUnlocked()
    {
        var lastUnlocked = LevelDataViews.Last(e => !e.Key.Level.Locked);
        lastUnlocked.Value.GetComponent<Button>().Select();
    }

    private GameObject AddUnlockedItemToView(LevelData item)
    {
        GameObject newCell = Instantiate(SceneData.Storage.CellPrefabUnlocked);
        newCell.GetComponent<Button>().onClick.AddListener(() => Select(item));
        newCell.transform.SetParent(SceneData.Storage.ContentView.transform, false);
        SetStars(item, newCell);
        return newCell;
    }

    private static void SetStars(LevelData item, GameObject view)
    {
        var star1 = view.GetComponentsInChildren<Transform>().Where(t => t.tag == Tags.Star1).First();
        var star2 = view.GetComponentsInChildren<Transform>().Where(t => t.tag == Tags.Star2).First();
        var star3 = view.GetComponentsInChildren<Transform>().Where(t => t.tag == Tags.Star3).First();
        if (item.Level.Completed)
        {
            switch (item.Level.Stars)
            {
                case 1:
                    star2.gameObject.SetActive(false);
                    star3.gameObject.SetActive(false);
                    break;
                case 2:
                    star3.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }
    }

    private GameObject AddLockedItemToView()
    {
        GameObject newCell = Instantiate(SceneData.Storage.CellPrefabLocked);
        newCell.transform.SetParent(SceneData.Storage.ContentView.transform, false);
        return newCell;
    }

    public void Select(LevelData item)
    {
        CurrentLevelData = item;
        SceneData.Storage.SceneFader.FadeTo(item.AssociatedScene);
    }

    public void CompleteLevel()
    {
        CurrentLevelData.Complete(LevelVariables.Instance.Stars, LevelVariables.Instance.Score);
        if (NextLevelData != null)
        {
            NextLevelData.Unlock();
        }
    }
}
