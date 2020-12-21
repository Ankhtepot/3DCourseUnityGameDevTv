using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class WeaponSwitcher : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int currentWeapon = 0;
    [SerializeField] public UnityTypeOfAmmoEvent OnWeaponSwitched;
#pragma warning restore 649

    void Start()
    {
        SetWeaponActive();
    }
    
    void Update()
    {
        int previousWeapon = currentWeapon;

        ManageInput();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ManageInput()
    {
        ProcessKeyInput();
        ProcessScrollwheel();
    }

    private void ProcessScrollwheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;
        
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
                OnWeaponSwitched?.Invoke(weapon.GetComponent<Weapon>().GetAmmoType());
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    
    
    private void initialize()
    {
       
    }
}
