using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureStats
{
    [Header("Stats")]
    [SerializeField, Range(1, 100), ReadOnlyWhenPlaying]
    private int particleCount = 20;
    public int ParticleCount { get => particleCount; set => particleCount = value; }

    [SerializeField, Range(0.0001f, 3), ReadOnlyWhenPlaying]
    private float movementSpeed = 1f;
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    [SerializeField, ReadOnly]
    private float updateStateTime = 1f;
    public float UpdateStateTime
    {
        get
        {
            updateStateTime = 0.5f / MovementSpeed;
            return updateStateTime;
        }
    }

    [SerializeField, Range(1, 3), Space, ReadOnlyWhenPlaying]
    private float viewRange = 1f;
    public float ViewRange { get => viewRange; set => viewRange = value; }

    [SerializeField, Range(1, 360), ReadOnlyWhenPlaying]
    private float viewAngle = 360f;
    public float ViewAngle { get => viewAngle; set => viewAngle = value; }

    [SerializeField, Range(1, 10000), Space, ReadOnlyWhenPlaying]
    private int baseHealth = 1;
    public int BaseHealth { get => baseHealth; set => baseHealth = value; }

    [SerializeField, Range(0, 10000), ReadOnlyWhenPlaying]
    private int attack = 0;
    public int Attack { get => attack; set => attack = value; }

    [SerializeField]
    private bool isDraggable = true;
    public bool IsDraggable { get => isDraggable; set => isDraggable = value; }

    [SerializeField, ReadOnlyWhenPlaying]
    private bool canRotateInMovement = true;
    public bool CanRotateInMovement { get => canRotateInMovement; set => canRotateInMovement = value; }
}
