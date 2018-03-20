using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Node
{
    public Vector3 Position { get; set; }
    public Node CameFrom { get; set; }
    //G score
    public float LengthFromStart { get; set; }
    //H score
    public float HeuristicLength { get; set; }
    //F=G+H
    public float FullLength => LengthFromStart + HeuristicLength;
}

