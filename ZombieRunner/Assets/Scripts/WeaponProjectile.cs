using System.Collections;
using Enumerations;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class WeaponProjectile : MonoBehaviour
{
#pragma warning disable 649
    public int damage;
    [SerializeField] private TypeOfDamage typeOfDamage;
    [SerializeField] private ParticleSystem HitEffect;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float lifetime = 2f;
#pragma warning restore 649

    private void Start()
    {
        StartCoroutine(LifetimeCooldown());
    }

    private void OnCollisionEnter(Collision other)
    {
        var enemy = other.gameObject.GetComponent<EnemyHealth>();
        if (enemy)
        {
            enemy.TakeDamage(damage, typeOfDamage);
            damage = 0;
        }

        SpawnHitEffect(other.contacts[0]);
        
        Destroy(gameObject);
    }

    private IEnumerator LifetimeCooldown()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

    private void SpawnHitEffect(ContactPoint collisionPoint)
    {
        if (!HitEffect) return;

        var effect = Instantiate(HitEffect, collisionPoint.point, Quaternion.Euler(collisionPoint.normal));
    }
}
