using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com
[RequireComponent(typeof(SnapToGrid))]
public class EnemyMovement : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private List<Waypoint> path;
    [SerializeField] int currentWaypointIndex;
    [SerializeField] int nextWaypointIndex;
    // [SerializeField] bool isMoving = false;
    [SerializeField] private float speed = 1;
    [SerializeField] private float timeForOneWaypointInSeconds = 1f;
    [SerializeField] private int stepsPerUnit = 30;
    private PathFinder pathFinder;
#pragma warning restore 649

    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        // pathFinder.OnPathFinderInitialized.AddListener(StartMovement);
        StartMovement();
    }

    IEnumerator FollowPath()
    {
        while (currentWaypointIndex < path.Count - 1)
        {
            yield return new WaitForSeconds(timeForOneWaypointInSeconds / stepsPerUnit);
            
            var newPosition = Vector3.MoveTowards(
                transform.position,
                path[nextWaypointIndex].transform.position, 
                speed * Time.deltaTime);
        
            transform.position = new Vector3(newPosition.x, 0, newPosition.z);

            // print($"Distance to next waypoint: {Vector3.Distance(transform.position, path[nextWaypoint].transform.position)}");
            
            if (!(Vector3.Distance(transform.position, path[nextWaypointIndex].transform.position) < 0.05f)) continue;

            currentWaypointIndex += 1;
            
            if (currentWaypointIndex >= path.Count) break;
            
            // print($"CurrentWaypoint is: {currentWaypoint}");
            
            if (nextWaypointIndex < path.Count - 1)
            {
                nextWaypointIndex += 1;
                transform.LookAt(path[nextWaypointIndex].transform.position);
            }
            else
            {
                transform.position = path[currentWaypointIndex].transform.position;
                // isMoving = false;
            }
        }
    }

    public void RestartPath()
    {
        // print("Enemy recalculating the path.");
        StartMovement();
    }

    private void StartMovement()
    {
        StopAllCoroutines();
        var alternativeStartPoint = path.Count == 0 ? null : path[nextWaypointIndex];
        path.Clear();
        SetPath(alternativeStartPoint);
        currentWaypointIndex = 0;
        
        nextWaypointIndex = 1;
       
        transform.LookAt(path[nextWaypointIndex].transform.position);
            
        // isMoving = true;
        
        StartCoroutine(FollowPath());
    }

    private void SetPath(Waypoint alternativeStartPoint)
    {
        //TODO: fix sometimes broken path - try set start point differently 
        var position = GetComponent<SnapToGrid>().PositionOnGrid;
        
        path = pathFinder.GetPath(position);

        if (path == null || path.Count <= 2)
        {
            path = pathFinder.GetPath(alternativeStartPoint.GridPosition);
            
            if ((path == null || path.Count <= 2))
            {
                Debug.LogWarning("Invalid path, cant follow.");
            }
        };
    }
}
