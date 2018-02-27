using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanControlller : BeingController
{
    public BoardManager boardManager;
    [Range(1f, 2f)]
    public float searchRadius = 1f;

    protected override BoardManager BoardManager => boardManager;
    protected override Transform Destination { get; set; }
    float SearchRadius => searchRadius;
    GameObject LastSeen { get; set; }

    protected override void Start()
    {
        Destination = GameObject.FindGameObjectWithTag(Tags.Vault).transform;
        base.Start();
    }
}
