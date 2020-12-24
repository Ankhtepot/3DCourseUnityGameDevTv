using System;
using System.Collections;
using System.Collections.Generic;
using Enumerations;
using TMPro;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class AmmoDisplayController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private TextMeshProUGUI AmmoTypeText;
    [SerializeField] private TextMeshProUGUI AmmoAmountText;
    [SerializeField] private Color bulletsColor;
    [SerializeField] private Color manaColor;

    private TypeOfAmmo currentTypeOfAmmo;
    private Ammo ammo;
    private WeaponSwitcher switcher;
#pragma warning restore 649

    void Start()
    {
        StartCoroutine(initialize());
    }

    void Update()
    {
        
    }

    private void UpdateAmmoType(TypeOfAmmo ammoType)
    {
        currentTypeOfAmmo = ammoType;
        Color typeColor;
        switch (ammoType)
        {
            case TypeOfAmmo.Bullets: typeColor = bulletsColor;
                break;
            case TypeOfAmmo.Mana: typeColor = manaColor;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ammoType), ammoType, null);
        }
        AmmoTypeText.text = ammoType.ToString();
        AmmoTypeText.color = typeColor;
        AmmoAmountText.color = typeColor;
        UpdateAmmoAmount(ammo.GetAmmoAmount(currentTypeOfAmmo), currentTypeOfAmmo);
    }

    private void UpdateAmmoAmount(int newAmount, TypeOfAmmo typeOfAmmo)
    {
        if (currentTypeOfAmmo == typeOfAmmo)
        {
            AmmoAmountText.text = newAmount.ToString();
        }
    }
    
    private IEnumerator initialize()
    {
        yield return new WaitForSeconds(0.2f);
        
        currentTypeOfAmmo = TypeOfAmmo.Bullets;
        ammo = FindObjectOfType<Ammo>();
        ammo.OnAmmoAmountChanged.AddListener(UpdateAmmoAmount);
        UpdateAmmoAmount(ammo.GetAmmoAmount(currentTypeOfAmmo), currentTypeOfAmmo);

        switcher = FindObjectOfType<WeaponSwitcher>();
        switcher.OnWeaponSwitched.AddListener(UpdateAmmoType);
        UpdateAmmoType(currentTypeOfAmmo);
    }
}
