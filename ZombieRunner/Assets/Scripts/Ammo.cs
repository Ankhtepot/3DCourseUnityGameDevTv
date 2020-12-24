using System.Collections.Generic;
using System.Linq;
using Enumerations;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class Ammo : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private List<AmmoType> ammoStore;
    [SerializeField] public UnityIntTypeOfAmmoEvent OnAmmoAmountChanged;
    
    private Dictionary<TypeOfAmmo, AmmoType> ammoDictionary = new Dictionary<TypeOfAmmo, AmmoType>();
#pragma warning restore 649

    void Start()
    {
        ammoDictionary = ammoStore.ToDictionary(ammo => ammo.TypeOfAmmo);
    }

    public bool ReduceAmmo(TypeOfAmmo typeOfAmmo, int reductionAmount = 1)
    {
        var ammoItem = ammoDictionary[typeOfAmmo];

        if (ammoItem != null)
        {
            var result = ammoItem.ReduceAmmo(reductionAmount);

            if (result)
            {
                OnAmmoAmountChanged?.Invoke(ammoItem.currentAmount, ammoItem.TypeOfAmmo);
            }
            
            return result;
        }
        
        Debug.LogWarning($"Ammo type {typeOfAmmo} not found.");
        return false;

    }

    public bool AddAmmo(TypeOfAmmo typeOfAmmo, int addedAmount)
    {
        var ammoItem = ammoDictionary[typeOfAmmo];

        if (ammoItem != null)
        {
            var result = ammoItem.AddAmmo(addedAmount);

            if (result)
            {
                OnAmmoAmountChanged?.Invoke(ammoItem.currentAmount, ammoItem.TypeOfAmmo);
            }
            
            return result;
        }
        
        Debug.LogWarning($"Ammo type {typeOfAmmo} not found.");
        return false;
    }

    public int GetAmmoAmount(TypeOfAmmo typeOfAmmo)
    {
        return ammoDictionary[typeOfAmmo]?.currentAmount ?? -1;
    }

    [System.Serializable]
    private class AmmoType
    {
        public TypeOfAmmo TypeOfAmmo;
        public int maxAmount;
        public int currentAmount;
        
        public bool ReduceAmmo(int reductionAmount = 1)
        {
            if (currentAmount < reductionAmount) return false;
        
            currentAmount -= reductionAmount;
        
            return true;
        }

        public bool AddAmmo(int addedAmount)
        {
            if (currentAmount == maxAmount) return false;

            currentAmount = Mathf.Min(maxAmount, currentAmount + addedAmount);

            return true;
        }
    }
}
