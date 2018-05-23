using System.Collections;
using System.Collections.Generic;
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
    }

    #endregion

    [System.Serializable]
    public class Pool
    {
        [SerializeField]
        private string tag;
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private int size;

        public string Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        public GameObject Prefab
        {
            get
            {
                return prefab;
            }
            set
            {
                prefab = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
    }

    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    [SerializeField]
    private List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> PoolDictionary => poolDictionary;
    public List<Pool> Pools => pools;

    private void Start()
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
}
