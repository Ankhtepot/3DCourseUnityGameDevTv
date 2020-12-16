using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public static class TowerStore
{
#pragma warning disable 649
    private static readonly List<Vector2Int> BuiltTowersPositions = new List<Vector2Int>();
#pragma warning restore 649
    
    public static void AddTower(Vector2Int towerGridPosition)
    {
        if (BuiltTowersPositions.Contains(towerGridPosition)) return;
        // Debug.Log($"[TowerStore] ADDED tower at position: {towerGridPosition}");
        BuiltTowersPositions.Add(towerGridPosition);
    }

    public static bool IsTowerOnGridPosition(Vector2Int gridPositionInQuestion)
    {
        return BuiltTowersPositions.Contains(gridPositionInQuestion);
    }

    public static void RemoveTower(Vector2Int towerGridPosition)
    {
        if (!BuiltTowersPositions.Contains(towerGridPosition))
        {
            Debug.LogWarning($"There is no tower registered at position: {towerGridPosition}");
            return;
        }

        BuiltTowersPositions.Remove(towerGridPosition);
    }
}