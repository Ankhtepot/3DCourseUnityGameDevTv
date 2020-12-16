using System.Collections;
using System.Linq;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Tower : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private AudioClip ShootSFX;
    [SerializeField] private Transform objectToPan;
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private Projectile BulletPrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fireDistance = 5f;
    [SerializeField] private float shootCooldown = 0.2f;
    [SerializeField] private float checkClosestEnemyCooldown = 0.5f;
    private bool isEnabled;
    private bool canShoot;
    private static readonly int ActivateTrigger = Animator.StringToHash("Activate");
#pragma warning restore 649
    
    private void Start()
    {
       
    }

    void Update()
    {
        FaceEnemy();
        Shoot();
    }

    public void Activate()
    {
        animator.SetBool("Activate", true);
        StartCoroutine(CheckClosestEnemy());
    }
    
    /// <summary>
    /// From animator event
    /// </summary>
    public void SetReadyState()
    {
        canShoot = true;
        isEnabled = true;
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private IEnumerator CheckClosestEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkClosestEnemyCooldown);

            var enemies = FindObjectsOfType<EnemyMovement>().ToList();
            if (enemies.Count > 0)
            {
                EnemyMovement closestEnemy = enemies[0];
                foreach (var enemy in enemies)
                {
                    var closestEnemyDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
                    if (Vector3.Distance(transform.position, enemy.transform.position) < closestEnemyDistance)
                    {
                        closestEnemy = enemy;
                    }
                }

                targetEnemy = closestEnemy.transform;
            }
        }
    }

    private void Shoot()
    {
        if (!(targetEnemy & isEnabled & canShoot & InRangeToFire()) || !BulletPrefab) return;

        if (audioSource && ShootSFX)
        {
            audioSource.clip = ShootSFX;
            audioSource.Play();
        }
        
        foreach (var gunPoint in gunPoints)
        {
            var bullet = Instantiate(BulletPrefab, gunPoint.position, Quaternion.identity);
            bullet.directionSource = gunPoint;
            bullet.PropelProjectile(gunPoint.forward);
        }

        canShoot = false;

        StartCoroutine(ShootCooldown());
    }

    private void FaceEnemy()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
        }
    }

    private bool InRangeToFire()
    {
        if (targetEnemy)
        {
            return Vector3.Distance(transform.position, targetEnemy.position) <= fireDistance;
        }

        return false;
    }
}
