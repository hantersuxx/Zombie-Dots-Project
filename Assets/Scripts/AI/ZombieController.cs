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

    protected override void OnDeath()
    {
        base.OnDeath();
        SpawnParticles();
    }

    protected void SpawnParticles()
    {
        for (int i = 0; i < ParticleCount; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(Tags.CreatureParticle, transform.position, Globals.ZombieParticleHexColor);
        }
    }

    public override void OnObjectSpawn(object transferValue)
    {
        base.OnObjectSpawn(transferValue);
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
