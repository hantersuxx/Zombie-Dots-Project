using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    public float Speed { get; set; }
    private Vector3? CurrentDestination { get; set; } = null;
    public bool InMove { get; private set; } = false;
    private delegate void MovementStateHandler();
    private event MovementStateHandler OnMovementStarted;
    private event MovementStateHandler OnMovementEnded;

    void Awake()
    {
        Speed = speed;
        OnMovementStarted += OnMovementStartedAction;
        OnMovementEnded += OnMovementEndedAction;
    }

    private IEnumerator MovementCoroutine()
    {
        OnMovementStarted();
        while (transform.position != CurrentDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination.Value, Speed * Time.deltaTime);
            yield return null;
        }
        OnMovementEnded();
    }

    public bool MoveTo(Vector3 destination)
    {
        if (CurrentDestination == null && !InMove)
        {
            CurrentDestination = destination;
            StartCoroutine(MovementCoroutine());
            return true;
        }
        return false;
    }

    public void StopMovement()
    {
        CurrentDestination = null;
        InMove = false;
        StopAllCoroutines();
    }

    private void OnMovementStartedAction()
    {
        InMove = true;
        RotateToDirection();
    }

    private void OnMovementEndedAction()
    {
        InMove = false;
        CurrentDestination = null;
    }

    private void RotateToDirection()
    {
        var relativePos = CurrentDestination.Value - transform.position;
        if (relativePos != Vector3.zero)
        {
            float angle = Mathf.Atan2(relativePos.x, relativePos.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(-angle, transform.forward);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(-angle, transform.forward), 75f * Time.deltaTime);
        }
    }
}
