using EpPathFinding.cs;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateController : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private CreatureStats stats;
    public CreatureStats Stats { get => stats; private set => stats = value; }
    //[Header("Stats")]
    //[SerializeField, Range(1, 100), ReadOnlyWhenPlaying]
    //private int particleCount = 20;
    //public int ParticleCount { get => particleCount; private set => particleCount = value; }

    //[SerializeField, Range(0.0001f, 3), ReadOnlyWhenPlaying]
    //private float movementSpeed = 1f;
    //public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    //[SerializeField]
    //private bool isDraggable = true;
    //public bool IsDraggable { get => isDraggable; set => isDraggable = value; }

    //[SerializeField, Range(1, 3), Space, ReadOnlyWhenPlaying]
    //private float viewRange = 1f;
    //public float ViewRange { get => viewRange; set => viewRange = value; }

    //[SerializeField, Range(1, 360), ReadOnlyWhenPlaying]
    //private float viewAngle = 360f;
    //public float ViewAngle { get => viewAngle; set => viewAngle = value; }

    //[SerializeField, Range(1, 1000), Space, ReadOnlyWhenPlaying]
    //private int baseHealth = 0;
    //public int BaseHealth { get => baseHealth; set => baseHealth = value; }

    [SerializeField, ReadOnly]
    private int currentHealth = 0;
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value <= 0)
            {
                OnDeath(EventArgs.Empty);
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    //[SerializeField, Range(0, 10000), ReadOnlyWhenPlaying]
    //private int attack = 0;
    //public int Attack { get => attack; private set => attack = value; }

    [SerializeField, ReadOnlyWhenPlaying]
    private State currentState;
    public State CurrentState { get => currentState; private set => currentState = value; }

    [SerializeField, ReadOnlyWhenPlaying]
    private State remainState;
    public State RemainState { get => remainState; private set => remainState = value; }

    [SerializeField, ReadOnly]
    private Transform chaseTarget;
    public Transform ChaseTarget { get => chaseTarget; set => chaseTarget = value; }

    public MovementAgent MovementAgent => GetComponent<MovementAgent>();

    public FieldOfView FOV => GetComponentInChildren<FieldOfView>();

    public virtual Queue<GridPos> WaypointList { get; set; } = new Queue<GridPos>();

    public bool IsActive { get; private set; } = false;

    public bool IsDead { get; private set; } = false;

    public event EventHandler TakingDamage;
    public event EventHandler Death;

    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        TakingDamage += HandleTakingDamage;
        Death += HandleDeath;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        TakingDamage -= HandleTakingDamage;
        Death -= HandleDeath;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        IsDead = false;
        MovementAgent.Speed = Stats.MovementSpeed;
        MovementAgent.CanRotateInMovement = Stats.CanRotateInMovement;
        FOV.ViewRange = Stats.ViewRange;
        FOV.ViewAngle = Stats.ViewAngle;
        CurrentHealth = Stats.BaseHealth;
    }

    private void FixedUpdate()
    {
        if (IsActive)
        {
            CurrentState.UpdateState(this);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (CurrentState != null && FOV != null)
        {
            Gizmos.color = CurrentState.SceneGizmoColor;
            Gizmos.DrawWireSphere(FOV.transform.position, FOV.ViewRange);
        }
    }

    public void SetupAI(bool aiActivation)
    {
        IsActive = aiActivation;
        if (aiActivation)
        {
            OnAIActivated();
        }
        else
        {
            OnAIDeactivated();
        }
    }

    private void OnAIActivated()
    {
        FOV.StartSearch();
        ChaseTarget = GameObject.FindGameObjectWithTag(Tags.Vault).transform;
        WaypointList = GetWaypointList(transform.position, ChaseTarget.position);
    }

    private void OnAIDeactivated()
    {
        FOV.StopSearch();
        MovementAgent.StopMovement();
        ChaseTarget = null;
        WaypointList = null;
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

    protected void SpawnParticles(string hexColor)
    {
        for (int i = 0; i < Stats.ParticleCount; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(Tags.CreatureParticle, transform.position, hexColor);
        }
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnTakingDamage(EventArgs.Empty);
    }

    protected virtual void OnDeath(EventArgs e)
    {
        Death?.Invoke(this, e);
    }

    protected virtual void HandleDeath(object sender, EventArgs e)
    {
        currentHealth = 0;
        IsDead = true;
        gameObject.SetActive(false);
        ObjectPooler.Instance.Destroy(tag, gameObject);
    }

    protected virtual void OnTakingDamage(EventArgs e)
    {
        TakingDamage?.Invoke(this, e);
    }

    protected virtual void HandleTakingDamage(object sender, EventArgs e)
    {

    }

    public void HandleObjectSpawn(object value)
    {
        if (value != null)
        {
            Stats = (CreatureStats)value;
        }
        Init();
        SetupAI(true);
    }

    public void HandleObjectDestroy()
    {
        SetupAI(false);
    }
}
