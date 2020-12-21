using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class PlayerHealth : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth = 100;
    public bool IsDead;
    [SerializeField] public UnityEvent OnPlayerDeath;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        Health -= damage;

        if (Health <= 0)
        {
            ManageDeath();
        }
    }

    private void ManageDeath()
    {
        IsDead = true;
        OnPlayerDeath?.Invoke();
        Debug.Log("Player died");
    }

    private void initialize()
    {
        Health = MaxHealth;
    }
}
