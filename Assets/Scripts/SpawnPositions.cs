using UnityEngine;


public static class SpawnPosition
{
    public static Vector3 Top => new Vector3(Random.Range(BoardManager.Instance.MinX, BoardManager.Instance.MaxX + 1), BoardManager.Instance.MaxY);
    public static Vector3 Left => new Vector3(BoardManager.Instance.MinX, Random.Range(BoardManager.Instance.MinY, BoardManager.Instance.MaxY + 1));
    public static Vector3 Right => new Vector3(BoardManager.Instance.MaxX, Random.Range(BoardManager.Instance.MinY, BoardManager.Instance.MaxY + 1));
}

[System.Flags]
public enum SpawnPositions
{
    Top = (1 << 0),
    Left = (1 << 1),
    Right = (1 << 2),
    Nearby = (1 << 3)
}