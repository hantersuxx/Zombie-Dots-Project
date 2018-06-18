using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public LevelsStorage Storage => FindObjectOfType<LevelsStorage>();

    [SerializeField]
    private LevelCollection levelCollection;
    public LevelCollection LevelCollection { get => levelCollection; set => levelCollection = value; }

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

        LoadSavedData();
    }

    private void OnDisable()
    {
        SaveLocally();
    }

    private void SaveLocally()
    {
        Serializer.SaveLocally(LevelCollection, Globals.LevelCollectionFileName);
    }

    private void LoadSavedData()
    {
        LoadLocallySavedData();
    }

    private void LoadLocallySavedData()
    {
        LevelCollection localData = Serializer.LoadLocally<LevelCollection>(Globals.LevelCollectionFileName);
        if (localData == null)
        {
            OnFirstLoad(LevelCollection);
        }
        else
        {
            OnDataExistsLoad(localData);
        }
    }

    private void OnFirstLoad(LevelCollection levelCollection)
    {
        for (int i = 0; i < levelCollection.LevelsData.Count; i++)
        {
            if (i == 0)
            {
                levelCollection.LevelsData[i].Level = Level.CreateUnlocked();
            }
            else
            {
                levelCollection.LevelsData[i].Level = Level.CreateLocked();
            }
        }
    }
    private void OnDataExistsLoad(LevelCollection localData)
    {
        if (localData.LevelsData.Count != LevelCollection.LevelsData.Count)
        {
            localData = MergeLocal(localData);
        }
        LevelCollection = localData;
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

    private GameObject AddUnlockedItemToView(LevelData item)
    {
        GameObject newCell = Instantiate(Storage.CellPrefabUnlocked);
        newCell.GetComponent<Button>().onClick.AddListener(() => Select(item));
        newCell.transform.SetParent(Storage.ContentView.transform, false);
        UpdateUnlockedView(item, newCell);
        return newCell;
    }

    private static void UpdateUnlockedView(LevelData item, GameObject view)
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
        GameObject newCell = Instantiate(Storage.CellPrefabLocked);
        newCell.transform.SetParent(Storage.ContentView.transform, false);
        return newCell;
    }

    public void Select(LevelData item)
    {
        CurrentLevelData = item;
        Storage.SceneFader.FadeTo(item.AssociatedScene);
    }

    public void CompleteLevel()
    {
        CurrentLevelData.Complete(LevelStats.Instance.Stars, LevelStats.Instance.Score);
        if (NextLevelData != null)
        {
            NextLevelData.Unlock();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveLocally();
        }
        else
        {

        }
    }
}
