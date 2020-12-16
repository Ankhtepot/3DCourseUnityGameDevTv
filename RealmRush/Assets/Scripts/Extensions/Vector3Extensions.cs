using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static Vector2Int ToGridVector2Int(this Vector3 originalPosition)
        {
            var gridSize = Waypoint.GridSize;
            return new Vector2Int(Mathf.FloorToInt(originalPosition.x / gridSize),Mathf.FloorToInt(originalPosition.z / gridSize));
        }
        
        public static Vector2Int ToVector2Int(this Vector3 originalPosition)
        {
            return new Vector2Int(Mathf.FloorToInt(originalPosition.x),Mathf.FloorToInt(originalPosition.z));
        }
    }
}