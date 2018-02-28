using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : BeingController
{
    [Range(1f, 2f)]
    public float searchRadius = 1f;

    protected override TileType[] Filters => new TileType[] { TileType.Obstruction };
    float SearchRadius => searchRadius;
    GameObject LastSeen { get; set; }
    bool IsTimerActive { get; set; } = false;
    float TimerSeconds { get; set; } = 0;

    protected override void Update()
    {
        if (LastSeen == null)
        {
            var human = Physics2D.OverlapCircleAll(transform.position, SearchRadius).FirstOrDefault(c => c.tag == Tags.Human)?.transform;
            if (human != null)
            {
                OnTargetHuman(human);
            }
        }
        else if (LastSeen != null && transform.position == LastSeen.transform.position)
        {
            Destroy(LastSeen);
            Destination = GameObject.FindGameObjectWithTag(Tags.Vault).transform;
            OnDestinationChanged();
        }
        base.Update();
    }

    protected override void OnMovement()
    {
        var currentPosition = Extensions.GetClosestPosition(transform.position, AllTilePositions);
        var tile = BoardManager.Tiles.FirstOrDefault(t => t.Position == currentPosition);
        tile.TileType = TileType.Zombie;
        base.OnMovement();
        tile.TileType = TileType.Ground;
    }

    private void OnTargetHuman(Transform human)
    {
        LastSeen = new GameObject();
        LastSeen.transform.position = Extensions.GetClosestPosition(human.transform.position, AllTilePositions);
        Destination = LastSeen.transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTimerActive = true;
        TimerSeconds += Time.deltaTime;
        if (collision.gameObject.tag == Tags.Human)
        {
            if (TimerSeconds >= 1)
            {
                Instantiate(this, collision.gameObject.transform.position, Quaternion.identity, GetComponentInParent<BoardManager>().transform);
                Destroy(collision.gameObject);
                TimerSeconds = 0;
            }
        }
        IsTimerActive = false;
    }
}
