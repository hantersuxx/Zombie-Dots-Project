﻿using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField]
    private bool completed = false;
    [SerializeField, Range(0, 3)]
    private int stars = 0;
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private bool locked = true;

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

    public static Level CreateLocked()
    {
        return new Level
        {
            Completed = false,
            Stars = 0,
            Score = 0,
            Locked = true
        };
    }

    public static Level CreateUnlocked()
    {
        return new Level
        {
            Completed = false,
            Stars = 0,
            Score = 0,
            Locked = false
        };
    }
}

