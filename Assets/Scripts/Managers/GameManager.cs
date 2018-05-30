using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

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
}
