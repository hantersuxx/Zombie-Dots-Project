using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject instance;
    StateController Controller => instance.GetComponent<StateController>();

    // Use this for initialization
    void Start()
    {
        var chaseTarget = GameObject.FindGameObjectWithTag(Tags.Vault.ToString()).transform;
        Controller.SetupAI(true, chaseTarget);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
