using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[ExecuteInEditMode]
public class EnvironmentStatics : MonoBehaviour
{
#pragma warning disable 649
    public List<Vector2Int> WaypointGrid = new List<Vector2Int>();
    public int GridSize = 10;
#pragma warning restore 649

    private void Start()
    {
        var waypoints = FindObjectsOfType<Waypoint>().ToList();
        WaypointGrid = waypoints.Select(waypoint => waypoint.GridPosition).ToList();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            WaypointGrid = FindObjectsOfType<Waypoint>().Select(waypoint => waypoint.GridPosition).ToList();
        }
    }
}
