using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float viewRange = 2f;
    public float ViewRange { get => viewRange; set => viewRange = value; }

    [SerializeField, Range(0, 360)]
    private float viewAngle = 120f;
    public float ViewAngle { get => viewAngle; set => viewAngle = value; }

    [SerializeField]
    private LayerMask targetMask;
    public LayerMask TargetMask { get => targetMask; set => targetMask = value; }

    [SerializeField]
    private LayerMask obstacleMask;
    public LayerMask ObstacleMask { get => obstacleMask; set => obstacleMask = value; }

    public List<Transform> VisibleTargets { get; private set; } = new List<Transform>();

    private IEnumerator Coroutine { get; set; }

    public void StartSearch()
    {
        Coroutine = FindTargetsWithDelay(0.2f);
        StartCoroutine(Coroutine);
    }

    public void StopSearch()
    {
        StopCoroutine(Coroutine);
    }

    private IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        VisibleTargets.Clear();
        Collider2D[] targetsInViewRange = Physics2D.OverlapCircleAll(transform.position, ViewRange, TargetMask);
        foreach (var collider in targetsInViewRange)
        {
            Transform target = collider.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < ViewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                var hit = Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, ObstacleMask);
                if (hit.transform == null && collider.gameObject.activeInHierarchy)
                {
                    VisibleTargets.Add(target);
                }
            }
        }
    }
}
