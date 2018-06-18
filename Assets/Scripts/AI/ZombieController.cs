using System;
using System.Collections.Generic;
using EpPathFinding.cs;

public class ZombieController : StateController
{
    public override Queue<GridPos> WaypointList
    {
        get
        {
            base.WaypointList = GetWaypointList(transform.position, ChaseTarget.position);
            base.WaypointList.Dequeue();
            return base.WaypointList;
        }
        set
        {
            base.WaypointList = value;
        }
    }

    protected override void HandleDeath(object sender, EventArgs e)
    {
        base.HandleDeath(sender, e);
        SpawnParticles(Globals.ZombieParticleHexColor);
    }
}
