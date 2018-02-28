using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BeingController : MonoBehaviour
{
    [Range(1f, 4f)]
    public float speedRange = 2f;

    protected abstract TileType[] Filters { get; }
    protected virtual BoardManager BoardManager { get; set; }
    protected virtual Transform Destination { get; set; }
    protected virtual float Speed { get; set; }
    protected virtual IEnumerable<Vector3> AllTilePositions { get; set; }
    protected virtual ShortestPath ShortestPath { get; set; }
    protected virtual List<Vector3> Route { get; set; }
    protected virtual Vector3 CurrentHeading { get; set; }
    protected virtual bool CanMove { get; set; } = false;

    protected virtual void Start()
    {
        BoardManager = GetComponentInParent<BoardManager>();
        Destination = GameObject.FindGameObjectWithTag(Tags.Vault).transform;
        Speed = UnityEngine.Random.Range(0.5f, speedRange);
        AllTilePositions = BoardManager.Tiles.Select(t => t.Position);
        ShortestPath = new ShortestPath(BoardManager.Tiles, BoardManager.TileSize, Filters);

        SetupRoute();
        CanMove = true;
    }

    protected virtual void Update()
    {
        if (transform.position == Destination.position)
        {
            OnDestinationArrival();
        }

        if (Route.Count != 0 && Destination.position != Route.LastOrDefault())
        {
            OnDestinationChanged();
        }

        if (CanMove)
        {
            OnMovement();
        }
    }

    protected virtual void OnDestinationArrival()
    {
        StopMovement();
    }

    protected virtual void OnDestinationChanged()
    {
        StopMovement();
        SetupRoute();
        AllowMovement();
    }
    protected virtual void OnMovement()
    {
        CurrentHeading = GetNextHeading();
        MoveTo(CurrentHeading);
    }

    private Vector3 GetNextHeading()
    {
        var returnValue = Route.FirstOrDefault();
        Route.Remove(returnValue);
        return returnValue;
    }

    private void MoveTo(Vector3 goal)
    {
        StartCoroutine(MovementCoroutine(goal));
    }

    private IEnumerator MovementCoroutine(Vector3 goal)
    {
        while (transform.position != goal)
        {
            transform.position = Vector3.MoveTowards(transform.position, goal, Speed * Time.deltaTime);
            CanMove = false;
            yield return null;
        }
        AllowMovement();
    }

    private void SetupRoute()
    {
        Route = ShortestPath.GetPath(transform.position, Destination.position).Reverse().ToList();
    }

    public virtual void DropDownOnClosestPosition()
    {
        transform.position = Extensions.GetClosestPosition(transform.position, AllTilePositions);
        SetupRoute();
    }

    public virtual void AllowMovement()
    {
        CanMove = true;
    }

    public virtual void StopMovement()
    {
        CanMove = false;
        StopAllCoroutines();
    }
}
