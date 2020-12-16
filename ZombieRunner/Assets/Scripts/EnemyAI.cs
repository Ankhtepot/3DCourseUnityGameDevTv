using System;
using UnityEngine;
using UnityEngine.AI;

//Fireball Games * * * PetrZavodny.com

public class EnemyAI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] float chaseRange = 5f;
    [SerializeField] private float stopDistanceOffset = 0.1f;
    [SerializeField] private float TurnSpeed = 5;
    [SerializeField] private bool isProvoked;
    [SerializeField] private bool PlayerIsDead;
    [SerializeField] Transform target;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    private float distanceToTarget = Mathf.Infinity;
    private PlayerHealth playerHealth;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        if (PlayerIsDead) return;
        
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if (isProvoked)
        {
            EngageTarget();
            FaceTarget();
        }
        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
    }

    private void EngageTarget()
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance + stopDistanceOffset)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }
    
    /// <summary>
    /// From EnemyHealth
    /// </summary>
    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void AttackTarget()
    {
        if (animator)
        {
            animator.SetBool("Attack", true);
        }

        if (playerHealth && playerHealth.IsDead)
        {
            PlayerIsDead = true;
        }
    }

    private void ChaseTarget()
    {
        animator.SetBool("Attack", false);
        animator.SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    private void initialize()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
