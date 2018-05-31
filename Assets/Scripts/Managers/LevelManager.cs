using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Loader")]
    [SerializeField]
    private GameObject cameraManager;

    [SerializeField]
    private GameObject objectPooler;

    [SerializeField]
    private GameObject boardManager;

    [Header("Stats")]
    [SerializeField]
    [Range(10, 100)]
    private int baseHealth = 10;

    [SerializeField]
    [Range(1, 1000)]
    private int goal = 10;

    [SerializeField]
    private bool isInfinite = false;

    [Header("Damage image")]
    [SerializeField]
    private Image damageImage;

    [SerializeField]
    private float flashSpeed = 0.5f;

    [SerializeField]
    private Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    [Header("Panels")]
    [SerializeField]
    private GameObject levelMenuPanel;
    [SerializeField]
    private GameObject pauseMenuPanel;
    [SerializeField]
    private GameObject gameOverPanel;

    private int currentHealth;
    private int currentScore;

    public int BaseHealth => baseHealth;
    public int Goal
    {
        get
        {
            return goal;
        }
        private set
        {
            goal = (value <= 0) ? 0 : value;
        }
    }
    public bool IsInfinite => isInfinite;
    public Image DamageImage => damageImage;
    public float FlashSpeed => flashSpeed;
    public Color FlashColor => flashColor;
    public bool Damaged { get; private set; } = false;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        private set
        {
            currentHealth = (value <= 0) ? 0 : value;
        }
    }
    public int Score
    {
        get
        {
            return currentScore;
        }
        private set
        {
            currentScore = (value <= 0) ? 0 : value;
        }
    }
    public GameObject LevelMenuPanel => levelMenuPanel;
    public GameObject PauseMenuPanel => pauseMenuPanel;
    public GameObject GameOverPanel => gameOverPanel;
    public int ZombiesKilled { get; private set; } = 0;
    public int HumansKilled { get; private set; } = 0;
    public bool GameIsPaused { get; set; } = false;
    public bool GameIsOver { get; private set; } = false;
    public bool LevelFinished { get; private set; } = false;
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
        CurrentHealth = BaseHealth;
    }

    private void Update()
    {
        if (GameIsPaused || GameIsOver || LevelFinished)
        {
            return;
        }

        if (CurrentHealth <= 0)
        {
            EndGame();
        }

        if (Goal <= 0)
        {
            FinishLevel();
        }

        if (Damaged)
        {
            DamageImage.color = FlashColor;
        }
        else
        {
            DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        Damaged = false;
    }

    private void EndGame()
    {
        GameIsOver = true;
        GameOverPanel.SetActive(true);
    }

    private void FinishLevel()
    {
        LevelFinished = true;
        //LevelFinishedPanel.SetActive(true);
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
        Score += value;
    }

    public void AchieveGoal()
    {
        Goal--;
    }

    public void KillZombie()
    {
        ZombiesKilled++;
    }

    public void KillHuman()
    {
        HumansKilled++;
    }

    public void TakeDamage(int amount)
    {
        Damaged = true;
        CurrentHealth -= amount;
    }

    public static void UpdateText(Text field, string text)
    {
        field.text = text;
    }
}
