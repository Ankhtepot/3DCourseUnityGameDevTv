using Enumerations;
using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class EnemyHealth : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private float DeathDelay = 1f;
    [SerializeField] public UnityEvent OnDeath;

    private bool isDead;
    private int originalHealth;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    public void TakeDamage(int damage, TypeOfDamage typeOfDamage = TypeOfDamage.Physical)
    {
        if (isDead) return;
        
        BroadcastMessage("OnDamageTaken");
        hitPoints = Mathf.Max(0, hitPoints - damage);
        print($"{name} took {damage} points of damage, resulting in remaining health of {hitPoints} hit points.");
        if (hitPoints <= 0)
        {
            ProcessDeath();
        }
    }

    private void ProcessDeath()
    {
        isDead = true;

       OnDeath?.Invoke();
        
        Destroy(gameObject, DeathDelay);
    }

    private void initialize()
    {
        originalHealth = hitPoints;
    }
}
