using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enumerations;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Ammo : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private List<AmmoType> ammoStore;
    
    private Dictionary<TypeOfAmmo, AmmoType> ammoDictionary = new Dictionary<TypeOfAmmo, AmmoType>();
#pragma warning restore 649

    void Start()
    {
        ammoDictionary = ammoStore.ToDictionary(ammo => ammo.TypeOfAmmo);
    }

    public bool ReduceAmmo(TypeOfAmmo typeOfAmmo, int reductionAmount = 1)
    {
        var ammoItem = ammoDictionary[typeOfAmmo];

        if (ammoItem != null) return ammoItem.ReduceAmmo();
        
        Debug.LogWarning($"Ammo type {typeOfAmmo} not found.");
        return false;

    }

    public bool AddAmmo(TypeOfAmmo typeOfAmmo, int addedAmount)
    {
        var ammoItem = ammoDictionary[typeOfAmmo];

        if (ammoItem != null) return ammoItem.AddAmmo(addedAmount);
        
        Debug.LogWarning($"Ammo type {typeOfAmmo} not found.");
        return false;

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
