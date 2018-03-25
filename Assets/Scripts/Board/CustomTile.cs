using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CustomTile
{
    public Vector3 Position { get; }
    public TileType TileType { get; set; }
    //public TileBase TileBase { get; }


    public CustomTile(Vector3 position, TileType tileType)
    {
        Position = position;
        TileType = tileType;
    }
}

public enum TileType
{
    Ground, Obstruction, Zombie, Human
}

