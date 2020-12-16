using Extensions;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Waypoint : MonoBehaviour
{
#pragma warning disable 649
    public bool IsExplored = false;
    public Waypoint ExploredFrom;
    private const string Top = "Top";
    public static readonly int GridSize = 10;
    public bool IsBlocked;
    public Vector2Int GridPosition => transform.position.ToGridVector2Int();
    private MouseGridPositionReceiver mouseGridPositionReceiver;
#pragma warning restore 649

    private void OnMouseEnter()
    {
        if (!mouseGridPositionReceiver)
        {
            mouseGridPositionReceiver = FindObjectOfType<MouseGridPositionReceiver>();
        }
        
        mouseGridPositionReceiver.RegisterNewPosition(GridPosition);
    }

    private void OnMouseExit()
    {
        mouseGridPositionReceiver.RegisterMouseLeft();
    }

    public void SetTopColor(Color color)
    {
        transform.Find(Top).GetComponent<MeshRenderer>().material.color = color;
    }

    public bool HasTowerMounted()
    {
        return TowerStore.IsTowerOnGridPosition(GridPosition);
    }
}