using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[ExecuteInEditMode]
[RequireComponent(typeof(SnapToGrid))]
public class RenameToGrid : MonoBehaviour
{
#pragma warning disable 649
    private SnapToGrid snapToGrid; 
#pragma warning restore 649

    void Start()
    {
        snapToGrid = GetComponent<SnapToGrid>();
    }

    void Update()
    {
        gameObject.name = $"{snapToGrid.PositionOnGrid.x},{snapToGrid.PositionOnGrid.y}";
    }
}
