using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    public static GameManager Instance { get; private set; } = null;
    public int Score { get; private set; }
    public Text ScoreText => scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("spawned");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("tried to spawn but instance already exist");
        }
        DontDestroyOnLoad(this);
    }

    public void AddScore(int addValue)
    {
        Score += addValue;
        //UpdateScore();
    }

    //private void UpdateScore()
    //{
    //    ScoreText.text = $"Score: {Score}";
    //}
}
