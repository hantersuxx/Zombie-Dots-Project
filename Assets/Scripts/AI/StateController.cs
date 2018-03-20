using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField]
    private State currentState;
    [SerializeField]
    private State remainState;
    [SerializeField]
    private Stats stats;
    [SerializeField]
    private Transform eyes;
    [SerializeField]
    private BoardManager boardManager;

    public State CurrentState { get; private set; }
    public State RemainState { get; private set; }
    public Stats Stats => stats;
    public Transform Eyes => eyes;
    public BoardManager BoardManager => boardManager;
    public MovementAgent MovementAgent => GetComponent<MovementAgent>();
    public FieldOfView FOV => GetComponentInChildren<FieldOfView>();
    public List<Vector3> WaypointList => BoardManager.ShortestPath.GetPath(transform.position, ChaseTarget.position);
    public int NextWaypoint { get; set; } = 0;
    private bool IsAIActive { get; set; } = false;
    public Transform ChaseTarget { get; set; }

    // Use this for initialization
    void Start()
    {
        CurrentState = currentState;
        RemainState = remainState;
    }

    void Update()
    {
        if (!IsAIActive)
        {
            return;
        }
        CurrentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (CurrentState != null && Eyes != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Eyes.position, Stats.lookSphereCastRadius);
        }
    }


    public void SetupAI(bool aiActivationFromZombieManager, List<Vector3> waypointListFromZombieManager)
    {
        IsAIActive = aiActivationFromZombieManager;
        //WaypointList = waypointListFromZombieManager;
    }

    public void SetupAI(bool aiActivationFromZombieManager, Transform chaseTarget)
    {
        IsAIActive = aiActivationFromZombieManager;
        ChaseTarget = chaseTarget;
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }
}
