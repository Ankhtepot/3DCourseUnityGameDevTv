using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class EnemyAttack : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private PlayerHealth target;
    [SerializeField] private int damage = 40;
#pragma warning restore 649

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (!target) return;
        target.TakeDamage(damage);
        Debug.Log("HIIIIIT from enemy");
    }
}
