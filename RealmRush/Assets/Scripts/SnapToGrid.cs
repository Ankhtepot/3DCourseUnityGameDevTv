using UnityEngine;

//Fireball Games * * * PetrZavodny.com
[ExecuteInEditMode]
[SelectionBase]
public class SnapToGrid : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float YOffset = 0f;
    public Vector2Int PositionOnGrid => GetPositionOnGrid();
    private Vector2Int positionOnGrid;
#pragma warning restore 649
    
    void Update()
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            Snap();
        }
    }
    
    private Vector2Int GetPositionOnGrid()
    {
        var gridSize = Waypoint.GridSize;
        var position = transform.position;
        var snappedPosition = new Vector2Int(Mathf.FloorToInt(position.x / gridSize),Mathf.FloorToInt(position.z / gridSize));
        // print($"NewPosition: {snappedPosition}");

        return snappedPosition;
    }

    private void Snap()
    {
        transform.position = new Vector3(GetPositionOnGrid().x * Waypoint.GridSize, YOffset, GetPositionOnGrid().y * Waypoint.GridSize);
    }
}
