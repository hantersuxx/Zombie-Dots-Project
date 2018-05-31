using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string levelName;
    [SerializeField]
    private bool completed;
    [SerializeField]
    private int stars;
    [SerializeField]
    private int score;
    [SerializeField]
    private bool locked;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
    public string LevelName
    {
        get
        {
            return levelName;
        }
        set
        {
            levelName = value;
        }
    }
    public bool Completed
    {
        get
        {
            return completed;
        }
        set
        {
            completed = value;
        }
    }
    public int Stars
    {
        get
        {
            return stars;
        }
        set
        {
            stars = value;
        }
    }
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            locked = value;
        }
    }

    public void Complete()
    {
        Completed = true;
    }

    public void Complete(int stars, int score)
    {
        Completed = true;
        Stars = stars;
        Score = score;
    }

    public void Lock()
    {
        Locked = true;
    }

    public void Unlock()
    {
        Locked = false;
    }
}
