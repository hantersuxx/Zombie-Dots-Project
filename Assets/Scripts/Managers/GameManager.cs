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

        //var bottom = BoardManager.Tiles.Where(t => t.Key.x == 0f).OrderBy(t => t.Key.y).First();
        var bottom = new Vector3(0, BoardManager.Instance.MinY);
        var chaseTarget = GameObject.FindGameObjectWithTag(Tags.Vault.ToString());
        chaseTarget.transform.position = bottom;

        SpawnPrefab(zombie, new Vector3(10, 4));
        SpawnPrefab(zombie, new Vector3(-10, 4));
        SpawnPrefab(human, new Vector3(8, 3));
    }

    private void SpawnPrefab(GameObject prefab, Vector3 position)
    {
        var chaseTarget = GameObject.FindGameObjectWithTag(Tags.Vault.ToString()).transform;
        var spawned = Instantiate(prefab, position, Quaternion.identity);
        spawned.GetComponent<StateController>().SetupAI(true);
    }
}
