using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "AI/Stats")]
public class Stats : MonoBehaviour
{
    [SerializeField]
    [Range(1, 3)]
    private float maxMoveSpeed = 1f;
    [SerializeField]
    [Range(1, 3)]
    private float maxViewRange = 1f;
    [SerializeField]
    [Range(0, 360)]
    private float viewAngle = 360f;
    [SerializeField]
    [Range(0, 1)]
    private int attack = 0;

    public float MoveSpeed { get; set; }
    public float ViewRange { get; set; }
    public float ViewAngle { get; set; }
    public int Attack { get; set; }

    private void Awake()
    {
        MoveSpeed = Random.Range(1f, maxMoveSpeed);
        ViewRange = Random.Range(1f, maxViewRange);
        ViewAngle = viewAngle;
        Attack = attack;
    }
}
