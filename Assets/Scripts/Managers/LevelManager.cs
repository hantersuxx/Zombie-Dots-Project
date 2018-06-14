using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelStorage Storage => FindObjectOfType<LevelStorage>();

    [Header("Loader")]
    [SerializeField]
    private GameObject cameraManager;

    [SerializeField]
    private GameObject objectPooler;

    [SerializeField]
    private GameObject boardManager;

    public bool Damaged { get; private set; } = false;

    public static LevelManager Instance { get; private set; } = null;

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

        InstantiateManager(cameraManager, CameraManager.Instance);
        InstantiateManager(objectPooler, ObjectPooler.Instance);
        InstantiateManager(boardManager, BoardManager.Instance);
    }

    private void Update()
    {
        if (LevelStats.Instance.GameIsPaused || LevelStats.Instance.GameIsOver)
        {
            return;
        }

        if (LevelStats.Instance.CurrentHealth <= 0)
        {
            EndGame();
        }

        if (LevelStats.Instance.GoalValue <= 0)
        {
            CompleteLevel();
        }

        if (Damaged)
        {
            Storage.DamageImage.color = Storage.FlashColor;
        }
        else
        {
            Storage.DamageImage.color = Color.Lerp(Storage.DamageImage.color, Color.clear, Storage.FlashSpeed * Time.deltaTime);
        }
        Damaged = false;
    }

    private void EndGame()
    {
        LevelStats.Instance.GameIsOver = true;
        ToggleGameOver();
    }

    private void CompleteLevel()
    {
        LevelStats.Instance.GameIsOver = true;
        AddScore(Globals.FinishLevelMultiplier * LevelStats.Instance.CurrentHealth);
        LevelsManager.Instance.CompleteLevel();
        ToggleCompleteLevel();
    }

    private void InstantiateManager<T>(GameObject gameObject, T staticInstance) where T : class
    {
        if (gameObject != null)
        {
            Instantiate(gameObject);
        }
    }

    public void AddScore(int value)
    {
        LevelStats.Instance.Score += value;
    }

    public void AchieveGoal()
    {
        LevelStats.Instance.GoalValue--;
    }

    public void KillZombie()
    {
        LevelStats.Instance.ZombiesKilled++;
    }

    public void KillHuman()
    {
        LevelStats.Instance.HumansKilled++;
    }

    public void SaveHuman()
    {
        LevelStats.Instance.HumansSaved++;
    }

    public void TakeDamage(int amount)
    {
        Damaged = true;
        LevelStats.Instance.CurrentHealth -= amount;
        //HealthBar.fillAmount = (float)CurrentHealth / BaseHealth;
    }

    public void TogglePauseMenu()
    {
        Storage.PauseMenu.SetActive(!Storage.PauseMenu.activeSelf);
        Storage.LevelMenu.SetActive(!Storage.LevelMenu.activeSelf);

        if (Storage.PauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            LevelStats.Instance.GameIsPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            LevelStats.Instance.GameIsPaused = false;
        }
    }

    public void ToggleGameOver()
    {
        Storage.GameOver.SetActive(!Storage.GameOver.activeSelf);
        Storage.LevelMenu.SetActive(!Storage.LevelMenu.activeSelf);

        if (Storage.GameOver.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ToggleCompleteLevel()
    {
        Storage.CompleteLevel.SetActive(!Storage.CompleteLevel.activeSelf);
        Storage.LevelMenu.SetActive(!Storage.LevelMenu.activeSelf);

        if (Storage.CompleteLevel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public static void UpdateText(Text field, string text)
    {
        field.text = text;
    }

    public static IEnumerator AnimateText(Text text, int boundaryValue)
    {
        text.text = "0";
        int value = 0;

        yield return new WaitForSeconds(.7f);

        while (value < boundaryValue)
        {
            value++;
            text.text = value.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (!focus)
    //    {
    //        if (!LevelStats.Instance.GameIsPaused && !LevelStats.Instance.GameIsOver && !LevelStats.Instance.LevelIsFinished)
    //        {
    //            TogglePauseMenu();
    //            SaveProgress();
    //        }
    //    }
    //}

    private void SaveProgress()
    {

    }
}
