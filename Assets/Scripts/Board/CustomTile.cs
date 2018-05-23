﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CustomTile
{
    public Vector3 Position { get; }
    public string TileType { get; set; }
    //public TileBase TileBase { get; }


    public CustomTile(Vector3 position, string tileType)
    {
        Position = position;
        TileType = tileType;
    }
}

