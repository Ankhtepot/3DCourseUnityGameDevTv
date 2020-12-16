using System;
using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class MouseGridPositionReceiver : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Vector2Int mouseGridPosition;
    [SerializeField] public UnityVector2IntEvent OnMousePositionChanged;
    [SerializeField] public UnityEvent OnMouseLeftWaypoint;
#pragma warning restore 649
    [Serializable]
    public class UnityVector2IntEvent : UnityEvent<Vector2Int> { }

    public void RegisterNewPosition(Vector2Int newMouseGridPosition)
    {
            mouseGridPosition = newMouseGridPosition;
            OnMousePositionChanged?.Invoke(mouseGridPosition);
    }

    public void RegisterMouseLeft()
    {
        OnMouseLeftWaypoint?.Invoke();
    }
}
