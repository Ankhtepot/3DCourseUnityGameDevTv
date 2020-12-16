using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[ExecuteInEditMode]
public class FieldCubeSides : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private GameObject LeftSide;
    [SerializeField] private GameObject RightSide;
    [SerializeField] private GameObject NearSide;
    [SerializeField] private GameObject FarSide;

    [SerializeField] private EnvironmentStatics statics;
#pragma warning restore 649

    private void Awake()
    {
        statics = FindObjectOfType<EnvironmentStatics>();
    }

    void Start()
    {
        ManageSides();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            ManageSides();
        }
    }

    private Vector2Int GetExploredPosition(Vector2Int direction)
    {
        return GetGridPosition() + direction;
    }

    private void ManageSides()
    {
        var grid = statics.WaypointGrid;
        FarSide.SetActive(grid.Contains(GetExploredPosition(Vector2Int.up)));
        NearSide.SetActive(grid.Contains(GetExploredPosition(Vector2Int.down)));
        LeftSide.SetActive(grid.Contains(GetExploredPosition(Vector2Int.left)));
        RightSide.SetActive(grid.Contains(GetExploredPosition(Vector2Int.right)));
    }
    
    private Vector2Int GetGridPosition()
    {
        var gridSize = statics.GridSize;
        return new Vector2Int()
        {
            x = Mathf.FloorToInt(transform.position.x / gridSize),
            y = Mathf.FloorToInt(transform.position.z / gridSize)
        };
    }
}
