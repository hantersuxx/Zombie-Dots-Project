using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanController : BeingController
{
    [Range(1f, 2f)]
    public float searchRadius = 1f;

    protected override TileType[] Filters => new TileType[] { TileType.Obstruction, TileType.Zombie };
    float SearchRadius => searchRadius;
    GameObject LastSeen { get; set; }

    protected override void Update()
    {
        var zombie = Physics2D.OverlapCircleAll(transform.position, SearchRadius).FirstOrDefault(c => c.tag == Tags.Zombie)?.gameObject;
        if (zombie != null)
        {
            OnDestinationChanged();
        }
        base.Update();
    }

    //private void OnTargetZombie(Transform zombie)
    //{
    //    LastSeen = new GameObject();
    //    LastSeen.transform.position = Extensions.GetClosestPosition(zombie.transform.position, AllTilePositions);
    //    Destination = LastSeen.transform;
    //}

}
