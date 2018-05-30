using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Loader")]
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private GameObject cameraManager;
    [SerializeField]
    private GameObject objectPooler;
    [SerializeField]
    private GameObject boardManager;
    [Header("Goals")]
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text goalText;
    [SerializeField]
    [Range(1, 1000)]
    private int goal = 10;
    [SerializeField]
    private bool isInfinite = false;
    private int score;

    public Text ScoreText => scoreText;
    public Text GoalText => goalText;
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
    public int Score
    {
        get
        {
            return score;
        }
        private set
        {
            score = (value <= 0) ? 0 : value;
        }
    }
    public bool LevelCompleted { get; private set; } = false;
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

        InstantiateManager(gameManager, GameManager.Instance);
        InstantiateManager(cameraManager, CameraManager.Instance);
        InstantiateManager(objectPooler, ObjectPooler.Instance);
        InstantiateManager(boardManager, BoardManager.Instance);
    }

    private void Update()
    {
        UpdateText(ScoreText, $"Score: {Score}");
        UpdateText(GoalText, $"{Goal} left to save");
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
        if (Goal <= 0)
        {
            LevelCompleted = true;
        }
    }

    public static void UpdateText(Text field, string text)
    {
        field.text = text;
    }
}
