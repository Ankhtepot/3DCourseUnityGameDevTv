using System;
using System.Collections;
using System.Collections.Generic;
using Enumerations;
using UnityEditor;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Weapon : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] protected int damage;
    [SerializeField] protected float range = 100f;
    [SerializeField] private float shotCooldown = 0.5f;
    [SerializeField] private TypeOfAmmo typeOfAmmo;
    [SerializeField] private int ammoConsumption = 1;
    [SerializeField] protected ParticleSystem muzzleFlashVFX;
    
    protected Camera FPCamera;
    private Ammo ammo;
    private bool canShoot = true;
#pragma warning restore 649

    private void Start()
    {
        var player = FindObjectOfType<PlayerHealth>();
        FPCamera = player.GetComponentInChildren<Camera>();
        ammo = player.GetComponent<Ammo>();
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (canShoot && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private IEnumerator CanShootCooldown()
    {
        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

    private void Shoot()
    {
        if (!ammo.ReduceAmmo(typeOfAmmo, ammoConsumption)) return;
        
        canShoot = false;
        StartCoroutine(CanShootCooldown());

        ProcessDamageDealing();
    }

    public TypeOfAmmo GetAmmoType()
    {
        return typeOfAmmo;
    }

    protected virtual void ProcessDamageDealing() {}
    

    
}
