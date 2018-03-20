using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "BoardManager")]
public class BoardManager : ScriptableObject
{
    [SerializeField]
    private float tileSizeX = 1f;
    [SerializeField]
    private float tileSizeY = 1f;

    public Vector3 TileSize => new Vector3(tileSizeX, tileSizeY);
    public IEnumerable<CustomTile> Tiles => InitList();
    public Camera Camera => Camera.allCameras[0];
    public ShortestPath ShortestPath => new ShortestPath(Tiles, TileSize);

    private IEnumerable<CustomTile> InitList()
    {
        var hashSet = new HashSet<CustomTile>();
        var bounds = Camera.OrthographicBounds();
        int minX = (int)Math.Ceiling(bounds.min.x),
            minY = (int)Math.Ceiling(bounds.min.y),
            maxX = (int)Math.Floor(bounds.max.x),
            maxY = (int)Math.Floor(bounds.max.y),
            cellSizeX = (int)TileSize.x,
            cellSizeY = (int)TileSize.y;
        for (int x = minX; x <= maxX; x += cellSizeX)
        {
            for (int y = minY; y <= maxY; y += cellSizeY)
            {
                var position = new Vector3Int(x, y, 0);
                var hasObstruction = Physics2D.OverlapCircleAll(new Vector2(position.x, position.y), cellSizeX / 2f)
                    .Any(t => t.gameObject.GetComponent<SpriteRenderer>().sortingLayerName == TileType.Obstruction.ToString());
                hashSet.Add(new CustomTile(position, (hasObstruction) ? TileType.Obstruction : TileType.Ground));
            }
        }
        return hashSet;
    }
}
