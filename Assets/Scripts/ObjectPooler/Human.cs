using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour, IPooledObject
{
    public HumanController HumanController { get; private set; }

    private void Awake()
    {
        HumanController = gameObject.GetComponent<HumanController>();
    }

    public void Destroy()
    {
        HumanController.SetupAI(false);
    }

    public void OnObjectSpawn(object transferValue)
    {
        HumanController.SetupAI(true);
    }
}
