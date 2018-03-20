using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/DefaultStats")]
public class Stats : ScriptableObject
{
    public float moveSpeed = 1f;
    public float viewRange = 2f;
    [Range(0, 360)]
    public float viewAngle = 120f;
    public float lookSphereCastRadius = 1f;

    //mb damage

    public float searchDuration = 4f;
    public float searchingSpeedTurnSpeed = 120f;
}
