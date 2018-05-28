using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IPooledObject
{
    public ZombieController ZombieController { get; private set; }

    private void Awake()
    {
        ZombieController = gameObject.GetComponent<ZombieController>();
    }

    public void Destroy()
    {
        ZombieController.SetupAI(false);
    }

    public void OnObjectSpawn(object transferValue)
    {
        ZombieController.SetupAI(true);
    }
}
