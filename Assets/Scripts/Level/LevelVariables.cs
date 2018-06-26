using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelVariables : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private string currentScene;
    public string CurrentScene { get => currentScene; set => currentScene = value; }

    [Space]
    [SerializeField, ReadOnlyWhenPlaying]
    private int baseHealth = 10;
    public int BaseHealth => baseHealth;

    [SerializeField, ReadOnlyWhenPlaying]
    private int tapDamage = 1;
    public int TapDamage => tapDamage;

    [SerializeField, ReadOnlyWhenPlaying]
    private int goalValue = 10;
    public int GoalValue { get => goalValue; set => goalValue = value; }

    [Space]
    [SerializeField, ReadOnly]
    private int currentHealth;
    public int CurrentHealth { get => currentHealth; set => currentHealth = (value <= 0) ? 0 : value; }

    [SerializeField, ReadOnly]
    private int score = 0;
    public int Score { get => score; set => score = (value <= 0) ? 0 : value; }

    [SerializeField, ReadOnly, Range(0, 3)]
    private int stars = 0;
    public int Stars
    {
        get
        {
            float star1 = 1f / 3f,
                star2 = 2f / 3f,
                star3 = 3f / 3f;
            float ratio = (float)CurrentHealth / BaseHealth;

            if (CurrentHealth <= 0)
            {
                stars = 0;
            }
            else if (CurrentHealth > 0 && ratio <= star1)
            {
                stars = 1;
            }
            else if (ratio > star1 && ratio <= star2)
            {
                stars = 2;
            }
            else if (ratio > star2 && ratio <= star3)
            {
                stars = 3;
            }
            return stars;
        }
    }

    [Space]
    [SerializeField, ReadOnly]
    private bool gameIsPaused = false;
    public bool GameIsPaused { get => gameIsPaused; set => gameIsPaused = value; }

    [SerializeField, ReadOnly]
    private bool gameIsOver = false;
    public bool GameIsOver { get => gameIsOver; set => gameIsOver = value; }

    [SerializeField, ReadOnly]
    private bool damaged = false;
    public bool Damaged { get => damaged; set => damaged = value; }

    public static LevelVariables Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        CurrentHealth = BaseHealth;
        GoalValue = goalValue;
    }
}
