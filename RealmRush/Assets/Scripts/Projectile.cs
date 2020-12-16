using System.Collections;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Projectile : DamageCollisionCollisionForwarderReceiverMono
{
#pragma warning disable 649
    [SerializeField] private Rigidbody rigidBody;
    public Transform directionSource;
    public int Damage;
    public float projectileSpeedMultiplier;
    [SerializeField] private float lifeTime = 0.3f;
#pragma warning restore 649

    private void Start()
    {
        StartCoroutine(LifetimeCooldown());
    }

    private IEnumerator LifetimeCooldown()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyBullet();
    }

    public void PropelProjectile(Vector3 direction)
    {
        rigidBody.AddRelativeForce(directionSource.transform.forward * projectileSpeedMultiplier);
    }

    public override void OnCollisionReceived(Collision other)
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
