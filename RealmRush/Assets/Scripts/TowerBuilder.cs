using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class TowerBuilder : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int maxTowers = 10;
    [SerializeField] private Vector2Int currentMouseGridPosition;
    [SerializeField] private float canPlaceTowerCooldown = 0.5f;
    [SerializeField] private TowerShopController towerShop;
    [SerializeField] private ResourcesController resourcesController;
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private ParticleSystem validGridPositionVFX;
    [SerializeField] private ParticleSystem invalidGridPositionVFX;
    private List<Vector2Int> startPoints = new List<Vector2Int>();
    private Tower previewTower;
    private bool canPlaceTower = true;
    private int placedTowersCount = 0;
#pragma warning restore 649

    private void Start()
    {
        SetStartAndEndPoints();
    }

    private void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (previewTower && canPlaceTower && Input.GetMouseButtonDown(0))
        {
            PlaceNewTower();
        }
    }

    private void PlaceNewTower()
    {
        canPlaceTower = false;
        var newTower = Instantiate(previewTower, currentMouseGridPosition.GridPositionToVector3(), Quaternion.identity);
        // var towerComponent = newTower.GetComponent<Tower>();
        
        if (!newTower /*|| !towerComponent*/) return;
        
        // towerComponent.Activate();
        newTower.Activate();
        
        TowerStore.AddTower(currentMouseGridPosition);
        placedTowersCount += 1;
        
        pathFinder.BlocksChanged = true;
        
        RefreshEnemyPaths();
        resourcesController.AddCrystalsAmount(-towerShop.SelectedItem.GetBuildCost());
        
        DestroyPreviewTower();
        SwitchVfxOff();
        
        OnMouseGridPositionChanged(currentMouseGridPosition);
        StartCoroutine(CanPlaceTowerCooldown());
    }

    private void RefreshEnemyPaths()
    {
        var enemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in enemies)
        {
            enemy.RestartPath();
        }
    }

    private IEnumerator CanPlaceTowerCooldown()
    {
        yield return new WaitForSeconds(canPlaceTowerCooldown);
        canPlaceTower = true;
    }

    private void SetStartAndEndPoints()
    {
        startPoints = FindObjectsOfType<EnemySpawnerController>().Select(spawner => spawner.GetSpawnPointPosition()).ToList();
    }

    /// <summary>
    /// From Event
    /// </summary>
    /// <param name="newGridPosition"></param>
    public void OnMouseGridPositionChanged(Vector2Int newGridPosition)
    {
        if (placedTowersCount >= maxTowers || TowerStore.IsTowerOnGridPosition(newGridPosition) || !HasResourcesForSelectedTower()) return;
        
        currentMouseGridPosition = newGridPosition;
        
        var validPath = !startPoints.Contains(newGridPosition) && IsPathValidIfWaypointIsOccupied();
        // print($"This waypoint is valid to place the tower: {validPath} | mouseGridPosition: {currentMouseGridPosition}");
        var activeVFX = validPath ? validGridPositionVFX : invalidGridPositionVFX;
        activeVFX.transform.position = newGridPosition.GridPositionToVector3();
        activeVFX.Play();

        if (validPath && towerShop.SelectedItem)
        {
            SpawnPreviewTower();
        }
        else if (previewTower)
        {
            DestroyPreviewTower();
        }
    }

    private void SpawnPreviewTower()
    {
        previewTower = Instantiate(
            towerShop.SelectedItem.towerPrefab,
            currentMouseGridPosition.GridPositionToVector3(),
            Quaternion.identity);
    }

    private bool HasResourcesForSelectedTower()
    {
        return resourcesController.GetCrystalsAmount() >= towerShop.SelectedItem.GetBuildCost();
    }

    /// <summary>
    /// From event
    /// </summary>
    public void OnMouseLeftWaypoint()
    {
        SwitchVfxOff();
        if (previewTower)
        {
            Destroy(previewTower.gameObject);
        }
    }

    private void SwitchVfxOff()
    {
        validGridPositionVFX.Stop();
        invalidGridPositionVFX.Stop();
    }

    private void DestroyPreviewTower()
    {
        if (!previewTower) return;
        
        Destroy(previewTower.gameObject);
        previewTower = null;
    }

    private bool IsPathValidIfWaypointIsOccupied()
    {
        foreach (var startPoint in startPoints)
        {
            if (!pathFinder.IsPathValid(startPoint, currentMouseGridPosition))
            {
                return false;
            }
        }

        return true;
    }
}
