using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class PathFinder : MonoBehaviour
{
#pragma warning disable 649
    public bool BlocksChanged = true;
    
    private Waypoint endWaypoint;
    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    private Queue<Waypoint> queue = new Queue<Waypoint>();
    private readonly List<Waypoint> path = new List<Waypoint>();
    private Vector2Int fathomTowerPosition;
    private readonly Vector2Int[] directions =
    {
       Vector2Int.up, 
       Vector2Int.right, 
       Vector2Int.down, 
       Vector2Int.left, 
    };
#pragma warning restore 649

    private void Start()
    {
        initialize();
    }

    private void initialize()
    {
        InitializeGrid();
        FindEndWaypoint();
    }

    private void FindEndWaypoint()
    {
        var endPoint = FindObjectOfType<EndWaypointController>();
        
        if (!endPoint)
        {
            Debug.LogWarning("EndWaypointController not found");
            return;
        }
        
        endWaypoint = grid
            .FirstOrDefault(waypoint => waypoint.Value.transform.position == endPoint.transform.position).Value;
        
        if (!endWaypoint)
        {
            Debug.LogWarning("EndWaypoint not found");
        }
    }

    public bool IsPathValid(Vector2Int startPosition, Vector2Int newFathomTowerPosition)
    {
        fathomTowerPosition = newFathomTowerPosition;
        BlocksChanged = true;
        
        var newPath = GetPath(startPosition, true);
        
        BlocksChanged = true;

        return newPath != null && newPath.Count >= 2;
    }

    public List<Waypoint> GetPath(Vector2Int startPosition, bool includeFathomTower = false)
    {
        if (BlocksChanged)
        {
            InitializeGrid();
            BlocksChanged = false;
        }
        
        // print($"Checking position: {position}");
        if (!grid.ContainsKey(startPosition)) return null;
        
        BreathFirstSearch(grid[startPosition], includeFathomTower);
        FindPath();
        
        return new List<Waypoint>(path);
    }

    private void BreathFirstSearch(Waypoint startWaypoint, bool includeFathomTower = false)
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.IsExplored = true;

            if (!EndPointFound(searchCenter))
            {
                QueueNeighbours(searchCenter, includeFathomTower);
                continue;
            }
            
            queue.Clear();
        }
    }

    private bool EndPointFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
        {
            // print("********** End point found *********");
            
            return true;
        }

        return false;
    }

    private void FindPath()
    {
        if (!endWaypoint)
        {
            Debug.LogWarning("No End waypoint set");
            return;
        }
        
        path.Clear();
        
        var currentWaypoint = endWaypoint;
        path.Add(currentWaypoint);
        
        while (currentWaypoint.ExploredFrom)
        {
            // print($"CurrentWaypoint.ExploredFrom: {currentWaypoint.ExploredFrom}");
            currentWaypoint = currentWaypoint.ExploredFrom;
            path.Add(currentWaypoint);
        }

        path.Reverse();
        
        foreach (var waypoint in grid.Values)
        {
            waypoint.IsExplored = false;
            waypoint.ExploredFrom = null;
        }
    }

    private void QueueNeighbours(Waypoint searchCenter, bool includeFathomTower = false)
    {
        foreach (var direction in directions)
        {
            var explorationCoordinates = direction + searchCenter.GridPosition;

            if (includeFathomTower && explorationCoordinates == fathomTowerPosition)
            {
                continue;
            }
            
            if (grid.ContainsKey(explorationCoordinates))
            {
                var foundWaypoint = grid[explorationCoordinates];
                
                if (!foundWaypoint.IsExplored || queue.Contains(foundWaypoint))
                {
                    queue.Enqueue(foundWaypoint);
                    foundWaypoint.ExploredFrom = searchCenter;
                }
            }
        }
    }

    private void InitializeGrid()
    {
        grid.Clear();
        
        var waypoints = FindObjectsOfType<Waypoint>().Where(waypoint => !TowerStore.IsTowerOnGridPosition(waypoint.GridPosition)).ToList();
        
        foreach (var waypoint in waypoints)
        {
            var position = waypoint.GridPosition;
            
            if (grid.ContainsKey(position))
            {
                Debug.LogWarning($"Overlapping waypoint at position: {waypoint}");
                continue;
            }
            
            grid.Add(position, waypoint);
        }
    }
}
