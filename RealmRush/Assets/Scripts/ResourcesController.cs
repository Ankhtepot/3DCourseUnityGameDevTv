using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class ResourcesController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int crystalsAmount = 9999;
    [SerializeField] private TextMeshProUGUI crystalsText;
    [SerializeField] private TowerShopController towerShop;
    [SerializeField] public UnityIntEvent OnCrystalAmountChanged;
#pragma warning restore 649
    [Serializable]
    public class UnityIntEvent : UnityEvent<int> { }

    void Start()
    {
        initialize();
    }

    public void AddCrystalsAmount(int amount)
    {
        crystalsAmount = Mathf.Clamp(crystalsAmount + amount, 0, 9999);
        RefreshCrystalsText();
        OnCrystalAmountChanged?.Invoke(crystalsAmount);
    }

    public int GetCrystalsAmount()
    {
        return crystalsAmount;
    }

    private void RefreshCrystalsText()
    {
        crystalsText.text = crystalsAmount.ToString();
    }
    
    private void initialize()
    {
        RefreshCrystalsText();
        OnCrystalAmountChanged?.Invoke(crystalsAmount);
    }
}
