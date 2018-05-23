using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton

    public static ObjectPooler Instance { get; set; } = null;

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
        Init();
    }

    #endregion

    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    [SerializeField]
    private List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> PoolDictionary => poolDictionary;
    public List<Pool> Pools => pools;

    private void Init()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, object transferValue = null)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = PoolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.identity;

        //TODO: get rid of getComponent
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject?.OnObjectSpawn(transferValue);

        PoolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public void Destroy(string tag, GameObject gameObject)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        GameObject objectToDestroy = PoolDictionary[tag].FirstOrDefault(g => g == gameObject);
        IPooledObject pooledObject = objectToDestroy?.GetComponent<IPooledObject>();
        pooledObject?.Destroy();
    }
}
