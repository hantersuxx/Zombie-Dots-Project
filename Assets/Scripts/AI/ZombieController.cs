using System.Collections;
using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

public class ZombieController : StateController
{
    public override Queue<GridPos> WaypointList
    {
        get
        {
            base.WaypointList = GetWaypointList(transform?.position, ChaseTarget?.position);
            base.WaypointList.Dequeue();
            return base.WaypointList;
        }
        set
        {
            base.WaypointList = value;
        }
    }
}
