using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VectorShortestPath
{
    public Vector3 Position { get; }
    public float MovementCost { get; }

    public VectorShortestPath(Vector3 position, float movementCost)
    {
        Position = position;
        MovementCost = movementCost;
    }
}

