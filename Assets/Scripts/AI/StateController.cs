using EpPathFinding.cs;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    [SerializeField]
    private State currentState;
    [SerializeField]
    private State remainState;

    public State CurrentState { get; private set; }
    public State RemainState { get; private set; }
    public Stats Stats { get; private set; }
    //public Transform Eyes { get; private set; }
    public MovementAgent MovementAgent { get; private set; }
    public FieldOfView FOV { get; private set; }
    public virtual Queue<GridPos> WaypointList { get; set; } = new Queue<GridPos>();
    public bool IsActive { get; private set; } = false;
    public Transform ChaseTarget { get; set; }

    private void Awake()
    {
        CurrentState = currentState;
        RemainState = remainState;
    }

    private void Start()
    {
        //Eyes = GetComponentInChildren<Transform>();
        MovementAgent = GetComponent<MovementAgent>();
        FOV = GetComponentInChildren<FieldOfView>();
        Stats = GetComponent<Stats>();
        Init();
    }

    private void Init()
    {
        MovementAgent.Speed = Stats.MoveSpeed;
        FOV.ViewRange = Stats.ViewRange;
        FOV.ViewAngle = Stats.ViewAngle;
    }

    private void FixedUpdate()
    {
        if (IsActive)
        {
            CurrentState.UpdateState(this);
        }
    }

    //protected virtual void OnDrawGizmos()
    //{
    //    if (CurrentState != null && Eyes != null)
    //    {
    //        Gizmos.color = CurrentState.SceneGizmoColor;
    //        Gizmos.DrawWireSphere(Eyes.position, Stats.lookSphereCastRadius);
    //    }
    //}

    public void SetupAI(bool aiActivation)
    {
        if (aiActivation)
        {
            OnAIActivated();
        }
        else
        {
            OnAIDeactivated();
        }
        IsActive = aiActivation;
    }

    private void OnAIActivated()
    {
        ChaseTarget = GameManager.Instance.Vault.transform;
        WaypointList = GetWaypointList(transform.position, ChaseTarget.position);
    }

    private void OnAIDeactivated()
    {
        MovementAgent.StopMovement();
        ChaseTarget = null;
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }

    public virtual Queue<GridPos> GetWaypointList(Vector3 start, Vector3 end)
    {
        Vector3Int startPos = Vector3Int.CeilToInt(start);
        Vector3Int endPos = Vector3Int.CeilToInt(end);
        BoardManager.Instance.JumpPointParam.Reset(new GridPos(startPos.x, startPos.y), new GridPos(endPos.x, endPos.y), BoardManager.Instance.Grid);
        return new Queue<GridPos>(JumpPointFinder.FindPath(BoardManager.Instance.JumpPointParam));
    }
}
