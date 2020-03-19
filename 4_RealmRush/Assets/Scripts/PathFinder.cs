using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    [SerializeField] Waypoint startWaypoint, endWaypoint;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Queue<Waypoint> queue = new Queue<Waypoint>();

    bool isRunning = true;
    Waypoint searchPoint;
    List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CaculatePath();
        }
        return path;
    }

    private void CaculatePath()
    {
        LoadBlocks();
        ColorStartandEnd();
        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(Waypoint point)
    {
        path.Add(point);
        point.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            searchPoint = queue.Dequeue();
            searchPoint.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
    }

    private void HaltIfEndFound()
    {
        if (searchPoint == endWaypoint)
        {
            isRunning = false;
        };
    }

    private void ExploreNeighbours()
    {
        if (!isRunning)
        {
            return;
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchPoint.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbour(neighbourCoordinates);
            }
        };
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if(neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        } 
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchPoint;
        }
        
    }

    private void ColorStartandEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Overlapping Waypoint "+ waypoint);
            } else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }
}
