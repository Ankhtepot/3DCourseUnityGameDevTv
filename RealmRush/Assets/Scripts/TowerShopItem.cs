using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Fireball Games * * * PetrZavodny.com

public class TowerShopItem : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private TowerShopController shop;
    [SerializeField] private Image Frame;
    [SerializeField] private TextMeshProUGUI CostTMP;
    [SerializeField] private bool IsSelected;
    [SerializeField] private bool isAvailable;
    public string DisplayedName;
    [SerializeField] private int buildCost;
    public Tower towerPrefab;
#pragma warning restore 649

    private void Awake()
    {
        CostTMP.text = buildCost.ToString();
    }

    public void SetSelected()
    {
        IsSelected = true;
        SetSelectedColor();
    }
    
    public void SetUnselected()
    {
        IsSelected = false;
        SetUnselectedColor();
    }

    public void SetAvailabilityColor(int availableResourceAmount)
    {
        isAvailable = availableResourceAmount >= buildCost;
        
        if (IsSelected)
        {
            SetSelectedColor();
        }
        else
        {
            SetUnselectedColor();
        }
    }

    private void SetUnselectedColor()
    {
        SetFrameColor(isAvailable ? shop.availableColor : shop.unavailableColor);
    }

    private void SetSelectedColor()
    {
        SetFrameColor(isAvailable ? shop.selectedAvailableColor : shop.selectedUnavailableColor);
    }

    private void SetFrameColor(Color newColor)
    {
        Frame.color = newColor;
    }

    public int GetBuildCost()
    {
        return buildCost;
    }

    /// <summary>
    /// From Event
    /// </summary>
    public void OnMouseClick()
    {
        shop.ChangeSelectedItem(this);
    }
}
