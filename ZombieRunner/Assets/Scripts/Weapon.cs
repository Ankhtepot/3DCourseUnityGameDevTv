using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Weapon : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int damage;
    [SerializeField] private float range = 100f;
    [SerializeField] private int bulletCount = 100;
    [SerializeField] private int maxBullets = 100;
    [SerializeField] private Camera FPCamera;
    [SerializeField] private ParticleSystem muzzleFlashVFX;
    [SerializeField] private GameObject hitEffect;
#pragma warning restore 649

    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletCount <= 0) return;

        bulletCount--;

        if (muzzleFlashVFX)
        {
            muzzleFlashVFX.Play();
        }
        
        RaycastHit hit;
        if (!Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) return;

        CreateHitImpact(hit);
        
        var enemyHealth = hit.transform.GetComponent<EnemyHealth>();

        if (!enemyHealth) return;
        
        print($"Shoot hit {hit.transform.gameObject.name}");
        enemyHealth.TakeDamage(damage);
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
