using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, ReadOnlyWhenPlaying]
    private SpawnObject[] spawnObjects;
    public SpawnObject[] SpawnObjects { get => spawnObjects; private set => spawnObjects = value; }

    [SerializeField]
    [Header("Delay between spawning (in seconds)")]
    private float timeBetweenWaves = 2;
    public float TimeBetweenWaves { get => timeBetweenWaves; private set => timeBetweenWaves = value; }

    [SerializeField, ReadOnly]
    private Vector3 spawnPosition;
    public Vector3 SpawnPosition
    {
        get
        {
            spawnPosition = new List<Vector3>() { SpawnPositions.Left, SpawnPositions.Top, SpawnPositions.Right }.PickRandom();
            return spawnPosition;
        }
    }

    public Dictionary<SpawnObject, float> SpawnObjectProbabilities { get; private set; } = new Dictionary<SpawnObject, float>();
    
    public int TotalRate { get; private set; }

    public bool IsSpawning { get; private set; } = false;
    
    private void Start()
    {
        TotalRate = SpawnObjects.Sum(o => o.Rate);
        foreach (var item in SpawnObjects)
        {
            SpawnObjectProbabilities.Add(item, (int)((float)item.Rate / TotalRate * 100));
        }
        Spawn();
    }

    public void Spawn()
    {
        IsSpawning = true;
        StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        IsSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnCoroutine()
    {
        //TODO: add boss spawn
        while (IsSpawning)
        {
            ObjectPooler.Instance.SpawnFromPool(
                DiceRoll(),
                SpawnPosition);
            yield return new WaitForSeconds(TimeBetweenWaves);
        }
    }

    private string DiceRoll()
    {
        int diceRoll = Random.Range(0, 101);
        float cumulative = 0;
        foreach (var item in SpawnObjectProbabilities)
        {
            cumulative += item.Value;
            if (diceRoll <= cumulative)
            {
                return item.Key.Tag;
            }
        }
        return DiceRoll();
    }
}
