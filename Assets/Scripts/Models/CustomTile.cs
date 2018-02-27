using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Models
{
    public class CustomTile
    {
        public Vector3 Position { get; }
        public bool IsOccupied { get; set; } = false;
        public TileBase TileBase { get; }


        public CustomTile(Vector3 position, TileBase tileBase)
        {
            Position = position;
            TileBase = tileBase;
        }
    }
}
