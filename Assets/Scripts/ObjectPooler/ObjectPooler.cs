using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    public Dictionary<string, Queue<GameObject>> PoolDictionary { get => poolDictionary; private set => poolDictionary = value; }

    [SerializeField]
    private List<Pool> pools;
    public List<Pool> Pools { get => pools; private set => pools = value; }

    public static ObjectPooler Instance { get; private set; } = null;

    private void Start()
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

    private void Init()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
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

    public GameObject SpawnFromPool(string tag, Vector3 position, object value = null)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.identity;
        objectToSpawn.SetActive(true);

        //TODO: get rid of getComponent
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject?.HandleObjectSpawn(value);

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
        pooledObject?.HandleObjectDestroy();
        objectToDestroy?.SetActive(false);
    }
}
