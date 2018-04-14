using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ShortestPath
{
    private BoardManager BoardManager { get; }

    public ShortestPath(BoardManager boardManager)
    {
        BoardManager = boardManager;
    }

    public Vector3[] GetPath(Vector3 start, Vector3 goal)
    {
        var positions = BoardManager.Tiles.Select(t => t.Key);
        start = Extensions.GetClosestPosition(start, positions);
        goal = Extensions.GetClosestPosition(goal, positions);
        var closedSet = new List<Node>();
        var startNode = new Node
        {
            Position = start,
            CameFrom = null,
            LengthFromStart = 0,
            HeuristicLength = GetHeuristicLength(start, goal)

        };
        var openSet = new List<Node>() { startNode };

        //while (openSet.Count > 0)
        //{
        //    var currentNode = openSet.OrderBy(node => node.FullLength).First();
        //    if (currentNode.Position == goal) { return GetPathForNode(currentNode).Reverse().ToArray(); }
        //    openSet.Remove(currentNode);
        //    closedSet.Add(currentNode);
        //    foreach (var successor in GetSuccessors(currentNode, goal))
        //    {
        //        if (closedSet
        //            .Where(node => node.Position.x == successor.Position.x && node.Position.y == successor.Position.y)
        //            .Count() > 0)
        //        {
        //            continue;
        //        }
        //        var openNode = openSet.FirstOrDefault(node => node.Position == successor.Position);

        //        if (openNode == null)
        //        {
        //            openSet.Add(successor);
        //        }
        //        else if (openNode.LengthFromStart >= successor.LengthFromStart)
        //        {
        //            openNode.CameFrom = currentNode;
        //            openNode.LengthFromStart = successor.LengthFromStart;
        //        }
        //    }
        //}

        while (openSet.Count > 0)
        {
            var currentNode = openSet.OrderBy(node => node.FullLength).First();
            openSet.Remove(currentNode);
            if (currentNode.Position == goal) { return GetPathForNode(currentNode).Reverse().ToArray(); }
            foreach (var successor in GetSuccessors(currentNode, goal))
            {
                var openNode = openSet.FirstOrDefault(node => node.Position == successor.Position);
                if (openNode != null)
                {
                    if (openNode.FullLength <= currentNode.FullLength)
                    {
                        continue;
                    }
                    openSet.Remove(openNode);
                }

                var closedNode = closedSet.FirstOrDefault(node => node.Position == successor.Position);
                if (closedNode != null)
                {
                    if (closedNode.FullLength <= currentNode.FullLength)
                    {
                        continue;
                    }
                    closedSet.Remove(closedNode);
                }
                openSet.Add(successor);
            }
            closedSet.Add(currentNode);
        }

        return null;
    }

    private IEnumerable<Vector3> GetPathForNode(Node node)
    {
        var currentNode = node;
        while (currentNode != null)
        {
            yield return currentNode.Position;
            currentNode = currentNode.CameFrom;
        }
    }

    private float GetHeuristicLength(Vector3 from, Vector3 to) => Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);

    private IEnumerable<Node> GetSuccessors(Node node, Vector3 goal)
    {
        IEnumerable<VectorShortestPath> expectedSuccessors = new List<VectorShortestPath>();
        expectedSuccessors = expectedSuccessors.AddIfNotNull(TryCreateSuccessor(new Vector3(node.Position.x + BoardManager.TileSizeX, node.Position.y), BoardManager.TileSizeX));
        expectedSuccessors = expectedSuccessors.AddIfNotNull(TryCreateSuccessor(new Vector3(node.Position.x - BoardManager.TileSizeX, node.Position.y), BoardManager.TileSizeX));
        expectedSuccessors = expectedSuccessors.AddIfNotNull(TryCreateSuccessor(new Vector3(node.Position.x, node.Position.y + BoardManager.TileSizeY), BoardManager.TileSizeY));
        expectedSuccessors = expectedSuccessors.AddIfNotNull(TryCreateSuccessor(new Vector3(node.Position.x, node.Position.y - BoardManager.TileSizeY), BoardManager.TileSizeY));
        //{
        //    new VectorShortestPath(new Vector3(node.Position.x + Step.x, node.Position.y), Step.x),
        //    new VectorShortestPath(new Vector3(node.Position.x - Step.x, node.Position.y), Step.x),
        //    new VectorShortestPath(new Vector3(node.Position.x, node.Position.y + Step.y), Step.y),
        //    new VectorShortestPath(new Vector3(node.Position.x, node.Position.y - Step.y), Step.y),
        //};

        foreach (var point in expectedSuccessors)
        {
            if (IsPositionAvailable(point.Position))
            {
                yield return new Node
                {
                    CameFrom = node,
                    Position = point.Position,
                    LengthFromStart = node.LengthFromStart + point.MovementCost,
                    HeuristicLength = GetHeuristicLength(point.Position, goal)
                };
            }
        }
    }

    private VectorShortestPath TryCreateSuccessor(Vector3 position, float movementCost)
    {
        if (IsPositionAvailable(position))
        {
            return new VectorShortestPath(position, movementCost);
        }
        return null;
    }

    //todo: add logic
    private bool IsPositionAvailable(Vector3 position)
    {
        //return Tiles
        //     .Any(t => t.TileType == TileType.Ground && t.Position == position);
        //var tile = BoardManager.Tiles.FirstOrDefault(t => t.Position == position);
        //return tile?.TileType == TileType.Ground;
        if (BoardManager.MinX <= position.x && position.x <= BoardManager.MaxX
            && BoardManager.MinY <= position.y && position.y <= BoardManager.MaxY)
        {
            return BoardManager.Tiles[position] == TileType.Ground;
        }
        return false;
    }
}

