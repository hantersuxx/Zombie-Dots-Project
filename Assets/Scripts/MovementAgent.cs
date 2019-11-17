using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private float speed = 1f;

    public float Speed { get; set; }
    public bool CanRotateInMovement { get; set; }
    private Vector3? CurrentDestination { get; set; } = null;
    public bool InMove { get; private set; } = false;

    private IEnumerator Coroutine { get; set; }

    public event EventHandler MovementStarted;
    public event EventHandler MovementEnded;

    protected virtual void OnMovementStarted(EventArgs e)
    {
        MovementStarted?.Invoke(this, e);
    }

    protected virtual void OnMovementEnded(EventArgs e)
    {
        MovementEnded?.Invoke(this, e);
    }

    private void Awake()
    {
        Speed = speed;
        MovementStarted += HandleMovementStarted;
        MovementEnded += HandleMovementEnded;
    }

    private void OnDestroy()
    {
        MovementStarted -= HandleMovementStarted;
        MovementEnded -= HandleMovementEnded;
    }

    private IEnumerator MovementCoroutine()
    {
        OnMovementStarted(EventArgs.Empty);
        while (transform.position != CurrentDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination.Value, Speed * Time.deltaTime);
            yield return null;
        }
        OnMovementEnded(EventArgs.Empty);
    }

    private void StartMovementCoroutine()
    {
        Coroutine = MovementCoroutine();
        StartCoroutine(Coroutine);
    }

    private void StopMovementCoroutine()
    {
        StopCoroutine(Coroutine);
    }

    public bool MoveTo(Vector3 destination)
    {
        if (CurrentDestination == null && !InMove)
        {
            CurrentDestination = destination;
            StartMovementCoroutine();
            return true;
        }
        return false;
    }

    public void StopMovement()
    {
        CurrentDestination = null;
        InMove = false;
        StopCoroutine(Coroutine);
    }

    private void HandleMovementStarted(object sender, EventArgs e)
    {
        InMove = true;
        RotateToDirection();
    }

    private void HandleMovementEnded(object sender, EventArgs e)
    {
        InMove = false;
        CurrentDestination = null;
    }

    private void RotateToDirection()
    {
        if (CanRotateInMovement)
        {
            var relativePos = CurrentDestination.Value - transform.position;
            if (relativePos != Vector3.zero)
            {
                float angle = Mathf.Atan2(relativePos.x, relativePos.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(-angle, transform.forward);
            }
        }
    }
}
