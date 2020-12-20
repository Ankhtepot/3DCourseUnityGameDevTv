﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class WeaponSwitcher : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int currentWeapon = 0;
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
            weapon.gameObject.SetActive(weaponIndex == currentWeapon);
            weaponIndex++;
        }

    }

    
    
    private void initialize()
    {
       
    }
}
