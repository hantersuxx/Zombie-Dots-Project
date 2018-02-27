using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : BeingController
{
    public BoardManager boardManager;
    [Range(1f, 2f)]
    public float searchRadius = 1f;

    protected override BoardManager BoardManager => boardManager;
    protected override Transform Destination { get; set; }
    float SearchRadius => searchRadius;
    GameObject LastSeen { get; set; }
    bool IsTimerActive { get; set; } = false;
    float TimerSeconds { get; set; } = 0;

    protected override void Start()
    {
        Destination = GameObject.FindGameObjectWithTag(Tags.Vault).transform;
        base.Start();
    }

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
                Instantiate(this).transform.position = collision.gameObject.transform.position;
                Destroy(collision.gameObject);
                TimerSeconds = 0;
            }
        }
        IsTimerActive = false;
    }
}
