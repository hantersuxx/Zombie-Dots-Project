using EpPathFinding.cs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    [SerializeField]
    private State currentState;
    [SerializeField]
    private State remainState;
    [SerializeField]
    private Stats stats;

    public virtual State CurrentState { get; private set; }
    public virtual State RemainState { get; private set; }
    public virtual Stats Stats => stats;
    public virtual Transform Eyes { get; private set; }
    public virtual MovementAgent MovementAgent { get; private set; }
    public virtual FieldOfView FOV { get; private set; }
    //public virtual Queue<GridPos> WaypointList => GetWaypointList(transform.position, ChaseTarget.position);

    //private Queue<GridPos> waypointList = new Queue<GridPos>();

    public virtual Queue<GridPos> WaypointList { get; set; } = new Queue<GridPos>();

    //public virtual int NextWaypoint { get; set; } = 0;
    private bool IsAIActive { get; set; } = false;
    public virtual Transform ChaseTarget { get; set; }

    protected virtual void Awake()
    {
        CurrentState = currentState;
        RemainState = remainState;
    }

    protected virtual void Start()
    {
        Eyes = GetComponentInChildren<Transform>();
        MovementAgent = GetComponent<MovementAgent>();
        FOV = GetComponentInChildren<FieldOfView>();
    }

    protected virtual void Update()
    {
        if (IsAIActive)
        {
            CurrentState.UpdateState(this);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (CurrentState != null && Eyes != null)
        {
            Gizmos.color = CurrentState.SceneGizmoColor;
            Gizmos.DrawWireSphere(Eyes.position, Stats.lookSphereCastRadius);
        }
    }

    public virtual void SetupAI(bool aiActivation)
    {
        if (aiActivation)
        {
            OnAIActivated();
        }
        else
        {
            OnAIDeactivated();
        }
        IsAIActive = aiActivation;
    }

    protected virtual void OnAIActivated()
    {
        ChaseTarget = GameManager.Instance.Vault.transform;
        WaypointList = GetWaypointList(transform.position, ChaseTarget.position);
    }

    protected virtual void OnAIDeactivated()
    {
        MovementAgent.StopMovement();
        ChaseTarget = null;
    }

    public virtual void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }
    public virtual Queue<GridPos> GetWaypointList(Vector3? start, Vector3? end)
    {
        Vector3Int? startPos = Vector3Int.CeilToInt(start.Value);
        Vector3Int? endPos = Vector3Int.CeilToInt(end.Value);
        BoardManager.Instance.JumpPointParam.Reset(new GridPos(startPos.Value.x, startPos.Value.y), new GridPos(endPos.Value.x, endPos.Value.y), BoardManager.Instance.Grid);
        return new Queue<GridPos>(JumpPointFinder.FindPath(BoardManager.Instance.JumpPointParam));
    }
}
