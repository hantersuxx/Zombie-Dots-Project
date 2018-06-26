using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelStorage Storage => FindObjectOfType<LevelStorage>();
    public LevelVariables LevelVariables => LevelVariables.Instance;

    [Header("Loader")]
    [SerializeField]
    private GameObject cameraManager;

    [SerializeField]
    private GameObject objectPooler;

    [SerializeField]
    private GameObject boardManager;

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
        if (LevelVariables.GameIsPaused || LevelVariables.GameIsOver)
        {
            return;
        }

        if (LevelVariables.CurrentHealth <= 0)
        {
            EndGame();
        }

        if (LevelVariables.GoalValue <= 0)
        {
            CompleteLevel();
        }

        if (LevelVariables.Damaged)
        {
            Storage.DamageImage.color = Storage.FlashColor;
        }
        else
        {
            Storage.DamageImage.color = Color.Lerp(Storage.DamageImage.color, Color.clear, Storage.FlashSpeed * Time.deltaTime);
        }
        LevelVariables.Damaged = false;
    }

    private void EndGame()
    {
        LevelVariables.GameIsOver = true;
        LevelVariables.Score = 0;
        ToggleGameOver();
    }

    private void CompleteLevel()
    {
        LevelVariables.GameIsOver = true;
        AddScore(Globals.FinishLevelMultiplier * LevelVariables.CurrentHealth);
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
        LevelVariables.Score += value;
    }

    public void AchieveGoal()
    {
        LevelVariables.GoalValue--;
    }

    public void KillZombie()
    {
        GameManager.Instance.IncreaseZombiesKilled();
    }

    public void KillHuman()
    {
        GameManager.Instance.IncreaseHumansKilled();
    }

    public void SaveHuman()
    {
        GameManager.Instance.IncreaseHumansSaved();
    }

    public void TakeDamage(int amount)
    {
        LevelVariables.Damaged = true;
        LevelVariables.CurrentHealth -= amount;
        //HealthBar.fillAmount = (float)CurrentHealth / BaseHealth;
    }

    public void TogglePauseMenu()
    {
        Storage.PauseMenu.SetActive(!Storage.PauseMenu.activeSelf);
        Storage.LevelMenu.SetActive(!Storage.LevelMenu.activeSelf);

        if (Storage.PauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            LevelVariables.GameIsPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            LevelVariables.GameIsPaused = false;
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

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && !LevelVariables.GameIsPaused && !LevelVariables.GameIsOver)
        {
            TogglePauseMenu();
        }
    }
}
