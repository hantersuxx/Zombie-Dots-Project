using System;
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

    [SerializeField, EnumFlags]
    private SpawnPositions positions;
    public SpawnPositions Positions { get => positions; private set => positions = value; }

    [SerializeField, ReadOnly]
    private Vector3 spawnVector;
    public Vector3 SpawnVector
    {
        get
        {
            var spawnList = new List<Vector3>();
            if (Positions.HasFlag(SpawnPositions.Left))
            {
                spawnList.Add(SpawnPosition.Left);
            }
            else if (Positions.HasFlag(SpawnPositions.Right))
            {
                spawnList.Add(SpawnPosition.Right);
            }
            else if (Positions.HasFlag(SpawnPositions.Top))
            {
                spawnList.Add(SpawnPosition.Top);
            }
            else if (Positions.HasFlag(SpawnPositions.Nearby))
            {
                Vector3 vector = BoardManager.Instance.GridDictionary.GetClosestPosition(
                    new Vector3(transform.position.x + UnityEngine.Random.insideUnitCircle.x * 2f,
                    transform.position.y + UnityEngine.Random.insideUnitCircle.y * 2f)).Key;
                spawnList.Add(vector);
            }
            spawnVector = spawnList.PickRandom();
            return spawnVector;
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
                SpawnVector);
            yield return new WaitForSeconds(TimeBetweenWaves);
        }
    }

    private string DiceRoll()
    {
        int diceRoll = UnityEngine.Random.Range(0, 101);
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
