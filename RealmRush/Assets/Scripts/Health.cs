using UnityEditor.SceneManagement;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class Health : DamageCollisionCollisionForwarderReceiverMono
{
#pragma warning disable 649
    [SerializeField] private int currentHealth;
    [SerializeField] private EnemyInfo info;
    [SerializeField] private ParticleSystem DeathVfx;
    [SerializeField] private AudioClip DeathSFX;
    [SerializeField] private float DeathVfxScaleRatio;
    [SerializeField] private ParticleSystem HitVfx;
    bool isDestroyed;
#pragma warning restore 649

    private void Start()
    {
    }

    public override void OnCollisionReceived(Collision other)
    {
        if (!other.gameObject.CompareTag("Harmful")) return;

        if (HitVfx)
        {
            Instantiate(HitVfx, other.transform.position,Quaternion.identity);    
        }
        
        currentHealth -= other.gameObject.GetComponentInParent<Projectile>().Damage;

        if (currentHealth <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            DisposeObject();
        }
    }

    private void DisposeObject()
    {
        if (DeathVfx)
        {
            var vfx = Instantiate(DeathVfx, transform.position, Quaternion.identity);
            vfx.transform.localScale = new Vector3(DeathVfxScaleRatio, DeathVfxScaleRatio, DeathVfxScaleRatio);
        }

        FindObjectOfType<SoundPlayer>().PlayOnce(DeathSFX, 1f);
        FindObjectOfType<ResourcesController>().AddCrystalsAmount(info.ResourceValue);
        
        Destroy(gameObject);
    }
}
