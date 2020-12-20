using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class ProjectileWeapon : Weapon
{
#pragma warning disable 649
    [SerializeField] private WeaponProjectile projectilePrefab;
    [SerializeField] private int projectileSpeed = 100;
    [SerializeField] private Transform projectileSpawnPosition;
#pragma warning restore 649

    protected override void ProcessDamageDealing()
    {
        if (muzzleFlashVFX)
        {
            muzzleFlashVFX.Play();
        }

        RaycastHit hit;

        if (!Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            var screenMiddle = FPCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)).GetPoint(1000);
            ShootProjectile(screenMiddle);
            return;
        }
        
        ShootProjectile(hit.point);
    }

    private void ShootProjectile(Vector3 targetPoint)
    {
        if (!projectilePrefab) return;

        var trailVector = targetPoint - projectileSpawnPosition.position;
        var newProjectile = Instantiate(projectilePrefab, projectileSpawnPosition.position,  Quaternion.LookRotation(trailVector));
        newProjectile.damage = damage;
        newProjectile.SetVelocity((trailVector).normalized * projectileSpeed);
    }
}
