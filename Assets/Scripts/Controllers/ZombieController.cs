using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : BeingController
{
    ////public Transform target;
    public BoardManager boardManager;

    //Vector3 Target { get; set; }
    //float SearchRadius => 2f;
    //BoardManager BoardManager => boardManager;
    //float Speed => Time.deltaTime * speed;
    //ShortestPath ShortestPath => new ShortestPath(BoardManager.Tiles, boardManager.TileSize);
    //IEnumerable<Vector3> Positions => BoardManager.Tiles.Select(t => t.Position);
    //List<Vector3> Routes { get; set; }
    //Vector3 CurrentRoute { get; set; }
    //bool CanMove { get; set; } = false;
    bool IsTargetHuman { get; set; } = false;

    //void Start()
    //{
    //    Target = GameObject.FindGameObjectWithTag(Tags.Vault).transform.position;
    //    speed = Random.Range(0.1f, speed);
    //    SetupRoutes();
    //    AllowMovement();
    //}

    //void Update()
    //{
    //    if (!IsTargetCivilian)
    //    {
    //        var civilian = Physics2D.OverlapCircleAll(transform.position, SearchRadius).FirstOrDefault(c => c.tag == Tags.Human)?.transform;
    //        if (civilian != null)
    //        {

    //            Target = Extensions.GetClosestPosition(civilian.transform.position, Positions);
    //            IsTargetCivilian = true;
    //        }
    //    }
    //}

    //private void MoveTo(Vector3 goal)
    //{
    //    StartCoroutine(MovementCoroutine(goal));
    //}

    //private IEnumerator MovementCoroutine(Vector3 goal)
    //{
    //    while (transform.position != goal)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, goal, Speed);
    //        CanMove = false;
    //        yield return null;
    //    }
    //    AllowMovement();
    //    if (IsTargetCivilian) { IsTargetCivilian = false; }
    //}

    //public void SetupRoutes()
    //{
    //    transform.position = Extensions.GetClosestPosition(transform.position, Positions);
    //    Routes = ShortestPath.GetPath(transform.position, Target).Reverse().ToList();
    //}

    //public void AllowMovement()
    //{
    //    CanMove = true;
    //}

    //public void StopMovement()
    //{
    //    CanMove = false;
    //    StopAllCoroutines();
    //}

    //private Vector3 GetNextRoute()
    //{
    //    var returnValue = Routes.FirstOrDefault();
    //    Routes.Remove(returnValue);
    //    return returnValue;
    //}
    protected override BoardManager BoardManager => boardManager;
    protected override Transform Destination { get; set; }

    protected override void Start()
    {
        Destination = GameObject.FindGameObjectWithTag(Tags.Vault).transform;
        base.Start();
    }

    protected override void Update()
    {

        base.Update();
    }
}
