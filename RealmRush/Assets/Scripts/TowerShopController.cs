using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class TowerShopController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] List<TowerShopItem> Items = new List<TowerShopItem>();
    [SerializeField] private TowerShopItem selectedItem;
    public Color availableColor;
    public Color unavailableColor;
    public Color selectedAvailableColor;
    public Color selectedUnavailableColor;

    [HideInInspector] public TowerShopItem SelectedItem
    {
        get => selectedItem;
        set => selectedItem = value;
    }
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        
    }
    
    private void initialize()
    {
        if (Items.Count == 0)
        {
            Debug.LogWarning("[Tower Shop]: No towers in the shop.");
            return;
        }

        selectedItem = Items[0];
        selectedItem.SetSelected();
    }

    public void SetTowerAvailability(int resourcesAmount)
    {
        if (Items.Count > 0)
        {
            Items.ForEach(item => item.SetAvailabilityColor(resourcesAmount));
        }
    }

    public void ChangeSelectedItem(TowerShopItem towerShopItem)
    {
        if (selectedItem == towerShopItem)
        {
            selectedItem.SetUnselected();
            return;
        }
        
        selectedItem.SetUnselected();
        towerShopItem.SetSelected();
        selectedItem = towerShopItem;
    }
}
