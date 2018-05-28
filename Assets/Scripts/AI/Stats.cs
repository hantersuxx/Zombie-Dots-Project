using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    [Range(1, 3)]
    private float maxMoveSpeed = 1f;
    [Header("Field of view")]
    [SerializeField]
    [Range(1, 3)]
    private float maxViewRange = 1f;
    [SerializeField]
    [Range(0, 360)]
    private float viewAngle = 360f;
    [Header("Attack")]
    [SerializeField]
    [Range(0, 1)]
    private int attack = 0;

    public float MoveSpeed { get; private set; }
    public float ViewRange { get; private set; }
    public float ViewAngle { get; private set; }
    public int Attack { get; private set; }

    private void Awake()
    {
        MoveSpeed = Random.Range(1f, maxMoveSpeed);
        ViewRange = Random.Range(1f, maxViewRange);
        ViewAngle = viewAngle;
        Attack = attack;
    }
}
