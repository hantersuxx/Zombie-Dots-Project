using UnityEngine;

[System.Serializable]
public class LevelData
{
    [SerializeField, Scene]
    private string associatedScene;
    [SerializeField]
    private Level level;

    public string AssociatedScene
    {
        get
        {
            return associatedScene;
        }

        set
        {
            associatedScene = value;
        }
    }

    public Level Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public void Complete()
    {
        Level.Completed = true;
    }

    public void Complete(int stars, int score)
    {
        Level.Completed = true;
        Level.Stars = stars;
        Level.Score = score;
    }

    public void Lock()
    {
        Level.Locked = true;
    }

    public void Unlock()
    {
        Level.Locked = false;
    }
}