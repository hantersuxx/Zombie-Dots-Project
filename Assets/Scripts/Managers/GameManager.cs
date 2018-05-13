using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private GameObject human;

    public static GameManager Instance { get; set; } = null;
    public GameObject Vault { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Vault = GameObject.FindGameObjectWithTag(Tags.Vault.ToString());
        //var bottom = BoardManager.Tiles.Where(t => t.Key.x == 0f).OrderBy(t => t.Key.y).First();
        var bottom = new Vector3(BoardManager.Instance.MaxX / 2, BoardManager.Instance.MinY);
        Vault.transform.position = bottom;

        StartCoroutine(SpawnCycle(0.1f));
    }

    private IEnumerator SpawnCycle(float delay)
    {
        for (int i = 0; i <= 2; i++)
        {

            SpawnPrefab(zombie, new Vector3(Random.Range(BoardManager.Instance.MinX, BoardManager.Instance.MaxX + 1), BoardManager.Instance.MaxY));
            SpawnPrefab(human, new Vector3(Random.Range(BoardManager.Instance.MinX, BoardManager.Instance.MaxX + 1), BoardManager.Instance.MaxY));
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnPrefab(GameObject prefab, Vector3 position)
    {
        var spawned = Instantiate(prefab, position, Quaternion.identity);
        if (spawned.tag == Tags.Human)
        {
            spawned.GetComponent<HumanController>().SetupAI(true);
        }
        else if (spawned.tag == Tags.Zombie)
        {
            spawned.GetComponent<ZombieController>().SetupAI(true);
        }
    }
}
