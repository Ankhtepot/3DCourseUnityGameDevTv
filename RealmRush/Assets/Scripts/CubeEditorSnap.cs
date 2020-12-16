using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditorSnap : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private bool ShowLabel = true;
    private TextMesh label;
    private Waypoint waypoint;
    private float gridSize;
#pragma warning restore 649

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        label = GetComponentInChildren<TextMesh>();
        gridSize = Waypoint.GridSize;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }
    
    private void SnapToGrid()
    {
        var gridPosition = waypoint.GridPosition;
        transform.position = new Vector3(gridPosition.x * this.gridSize, 0f, gridPosition.y * this.gridSize);
    }

    private void UpdateLabel()
    {
        if (ShowLabel)
        {
            var position = waypoint.GridPosition;
            var generatedName = $"{Mathf.FloorToInt(position.x)},{Mathf.FloorToInt(position.y)}";
            label.text = generatedName;
            gameObject.name = generatedName;
        }
        else
        {
            label.text = string.Empty;
        }
    }
}
