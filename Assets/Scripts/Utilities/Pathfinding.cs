using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    public static List<Vector3Int> FindPath(Vector3Int start, Vector3Int target)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            if (current == target)
            {
                while (cameFrom.ContainsKey(current))
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path;
            }

            foreach (var neighbor in GetNeighbors(current))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        return path;
    }

    private static List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        return new List<Vector3Int>
        {
            cell + Vector3Int.up,
            cell + Vector3Int.down,
            cell + Vector3Int.left,
            cell + Vector3Int.right
        };
    }
}
