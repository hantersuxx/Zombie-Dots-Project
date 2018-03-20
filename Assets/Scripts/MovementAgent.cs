using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField]
    private float speed;
    //[SerializeField]
    //private Transform eyes;

    public float Speed => speed;
    public Transform Eyes => GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "Eyes");
    public Vector3? CurrentDestination { get; private set; } = null;
    private bool InMove { get; set; } = false;
    private delegate void MovementStateHandler(bool state);
    private event MovementStateHandler OnMovementStarted;
    private event MovementStateHandler OnMovementEnded;

    void Start()
    {
        OnMovementStarted += SetMovementState;
        OnMovementEnded += SetMovementState;
    }

    void Update()
    {
        if (CurrentDestination != null && !InMove)
        {
            StartCoroutine(MovementCoroutine());
        }
    }

    public bool MoveTo(Vector3 destination)
    {
        if (CurrentDestination == null && !InMove)
        {
            CurrentDestination = destination;
            return true;
        }
        return false;
    }

    private IEnumerator MovementCoroutine()
    {
        OnMovementStarted(true);
        while (transform.position != CurrentDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentDestination.Value, Speed * Time.deltaTime);
            yield return null;
        }
        OnMovementEnded(false);
    }

    public void StopMovement()
    {
        StopAllCoroutines();
    }

    private void SetMovementState(bool state)
    {
        InMove = state;
        if (!state)
        {
            CurrentDestination = null;
        }
        else
        {
            RotateToDirection();
        }
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
