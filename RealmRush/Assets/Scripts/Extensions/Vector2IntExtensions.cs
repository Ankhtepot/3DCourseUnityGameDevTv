using UnityEngine;

namespace Extensions
{
    public static class Vector2IntExtensions
    {
        public static Vector3 GridPositionToVector3(this Vector2Int originalPosition)
        {
            var gridSize = Waypoint.GridSize;
            return new Vector3(originalPosition.x * gridSize, 0, originalPosition.y * gridSize);
        } 
    }
}