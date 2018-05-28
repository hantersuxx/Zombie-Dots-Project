using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    [Header("Delay between spawning (in seconds)")]
    private int intensity = 2;
    [SerializeField]
    [Range(1, 99)]
    [Header("Human / Zombie (100% - Human) probability to spawn")]
    private int probability = 50;

    public int Intensity => intensity;
    public int Probability => probability;

    private void Start()
    {
        VaultHealth vaultHealth = GameObject.FindGameObjectWithTag(Tags.Vault).GetComponent<VaultHealth>();
        StartCoroutine(SpawnCycle(vaultHealth));
    }

    private IEnumerator SpawnCycle(VaultHealth vaultHealth)
    {
        //TODO: remove
        while (true)
        //while (!vaultHealth.IsDead)
        {
            ObjectPooler.Instance.SpawnFromPool(
                DiceRoll(),
                new Vector3(Random.Range(BoardManager.Instance.MinX, BoardManager.Instance.MaxX + 1), BoardManager.Instance.MaxY));
            yield return new WaitForSeconds(Intensity);
        }
    }

    private string DiceRoll()
    {
        var spawnList = new List<KeyValuePair<string, int>>
        {
            new KeyValuePair<string,int>(Tags.Human, Probability),
            new KeyValuePair<string,int>(Tags.Zombie, 100 - Probability)
        };
        int diceRoll = Random.Range(0, 101);
        int cumulative = 0;
        for (int i = 0; i < spawnList.Count; i++)
        {
            cumulative += spawnList[i].Value;
            if (diceRoll <= cumulative)
            {
                return spawnList[i].Key;
            }
        }
        return null;
    }
}
