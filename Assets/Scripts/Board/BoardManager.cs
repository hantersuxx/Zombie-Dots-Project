using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private float tileSizeX = 1f;
    [SerializeField]
    private float tileSizeY = 1f;

    public static BoardManager Instance { get; set; } = null;

    Camera Camera => Camera.allCameras[0];

    public int TileSizeX { get; private set; }
    public int TileSizeY { get; private set; }
    public int MinX { get; private set; }
    public int MinY { get; private set; }
    public int MaxX { get; private set; }
    public int MaxY { get; private set; }

    public Dictionary<Vector3, TileType> Tiles { get; private set; }

    public ShortestPath ShortestPath => new ShortestPath(this);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        TileSizeX = (int)tileSizeX;
        TileSizeY = (int)tileSizeY;
        MinX = (int)Math.Ceiling(Camera.OrthographicBounds().min.x);
        MinY = (int)Math.Ceiling(Camera.OrthographicBounds().min.y);
        MaxX = (int)Math.Floor(Camera.OrthographicBounds().max.x);
        MaxY = (int)Math.Floor(Camera.OrthographicBounds().max.y);

        Tiles = new Dictionary<Vector3, TileType>();
        for (int x = MinX; x <= MaxX; x += TileSizeX)
        {
            for (int y = MinY; y <= MaxY; y += TileSizeY)
            {
                var position = new Vector3Int(x, y, 0);
                var hasObstruction = Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), TileSizeX / 2f)
                    .Any(t => t.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == TileType.Obstruction.ToString());
                Tiles.Add(position, (hasObstruction) ? TileType.Obstruction : TileType.Ground);
            }
        }
    }
}
