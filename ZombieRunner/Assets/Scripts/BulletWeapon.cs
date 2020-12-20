using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class BulletWeapon : Weapon
{
#pragma warning disable 649
    [SerializeField] private GameObject hitEffect;
#pragma warning restore 649

    protected override void ProcessDamageDealing()
    {
        if (muzzleFlashVFX)
        {
            muzzleFlashVFX.Play();
        }

        RaycastHit hit;

        if (!Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) return;
        
        var enemyHealth = hit.transform.GetComponent<EnemyHealth>();

        CreateHitImpact(hit);
        
        if (!enemyHealth) return;
        
        print($"Shoot hit {hit.transform.gameObject.name}");
        enemyHealth.TakeDamage(damage);
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        if (!hitEffect) return;
        
        var effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
