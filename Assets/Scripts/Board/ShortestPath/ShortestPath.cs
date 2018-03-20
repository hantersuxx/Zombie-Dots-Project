using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShortestPath
{
    private IEnumerable<CustomTile> Tiles { get; }
    private Vector3 Step { get; }

    public ShortestPath(IEnumerable<CustomTile> tiles, Vector3 step)
    {
        Tiles = tiles;
        Step = step;
    }

    public List<Vector3> GetPath(Vector3 start, Vector3 goal)
    {
        var positions = Tiles.Select(t => t.Position);
        start = Extensions.GetClosestPosition(start, positions);
        goal = Extensions.GetClosestPosition(goal, positions);
        var closedSet = new HashSet<Node>();
        var startNode = new Node
        {
            Position = start,
            CameFrom = null,
            LengthFromStart = 0,
            HeuristicLength = GetHeuristicLength(start, goal)

        };
        var openSet = new HashSet<Node>() { startNode };

        while (openSet.Count > 0)
        {
            var currentNode = openSet.OrderBy(node => node.FullLength).First();
            if (currentNode.Position == goal) { return GetPathForNode(currentNode).Reverse().ToList(); }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            foreach (var successor in GetSuccessors(currentNode, goal))
            {
                if (closedSet.Count(node => node.Position.x == successor.Position.x && node.Position.y == successor.Position.y) > 0) { continue; }
                var openNode = openSet.FirstOrDefault(node => node.Position == successor.Position);

                if (openNode == null)
                {
                    openSet.Add(successor);
                }
                else if (openNode.LengthFromStart >= successor.LengthFromStart)
                {
                    openNode.CameFrom = currentNode;
                    openNode.LengthFromStart = successor.LengthFromStart;
                }
            }
        }

        //while (openSet.Count > 0)
        //{
        //    var currentNode = openSet.OrderBy(node => node.FullLength).First();
        //    openSet.Remove(currentNode);
        //    if (currentNode.Position == goal) { return GetPathForNode(currentNode); }
        //    foreach (var successor in GetSuccessors(currentNode, goal))
        //    {
        //        var openNode = openSet.FirstOrDefault(node => node.Position == successor.Position);
        //        if (openNode != null)
        //        {
        //            if (openNode.FullLength <= currentNode.FullLength)
        //            {
        //                continue;
        //            }
        //            openSet.Remove(openNode);
        //        }

        //        var closedNode = closedSet.FirstOrDefault(node => node.Position == successor.Position);
        //        if (closedNode != null)
        //        {
        //            if (closedNode.FullLength <= currentNode.FullLength)
        //            {
        //                continue;
        //            }
        //            closedSet.Remove(closedNode);
        //        }
        //        openSet.Add(successor);
        //    }
        //    closedSet.Add(currentNode);
        //}

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
        var expectedSuccessors = new HashSet<VectorShortestPath>
            {
                new VectorShortestPath(new Vector3(node.Position.x + Step.x, node.Position.y), Step.x),
                new VectorShortestPath(new Vector3(node.Position.x - Step.x, node.Position.y), Step.x),
                new VectorShortestPath(new Vector3(node.Position.x, node.Position.y + Step.y), Step.y),
                new VectorShortestPath(new Vector3(node.Position.x, node.Position.y - Step.y), Step.y),
            };

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

    //todo: add logic
    private bool IsPositionAvailable(Vector3 position)
    {
        return !Tiles
             .Where(t => t.TileType == TileType.Obstruction && t.Position == position)
             .Any();
    }
}

