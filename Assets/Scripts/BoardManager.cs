using Assets.Scripts.Extensions;
using Assets.Scripts.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public Grid grid;

    public IEnumerable<CustomTile> Tiles => InitList();
    public Vector3 TileSize => Grid.cellSize;
    Grid Grid => grid;
    Camera Camera => Camera.allCameras[0];
    Tilemap[] Tilemaps => Grid.GetComponentsInChildren<Tilemap>();

    void Start()
    {
        Debug.Log(Tiles);
    }

    void Update()
    {
    }

    private IEnumerable<CustomTile> InitList()
    {
        var bounds = Camera.OrthographicBounds();
        int minX = (int)Math.Ceiling(bounds.min.x),
            minY = (int)Math.Ceiling(bounds.min.y),
            maxX = (int)Math.Floor(bounds.max.x),
            maxY = (int)Math.Floor(bounds.max.y),
            cellSizeX = (int)TileSize.x,
            cellSizeY = (int)TileSize.y;
        foreach (var tilemap in Tilemaps)
        {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            for (int x = minX; x <= maxX; x += cellSizeX)
            {
                for (int y = minY; y <= maxY; y += cellSizeY)
                {
                    var position = new Vector3Int(x, y, 0);
                    var tile = tilemap.GetTile<TileBase>(position);
                    if (tile != null)
                    {
                        yield return new CustomTile(position, tile);
                    }
                }
            }
        }
    }
}
