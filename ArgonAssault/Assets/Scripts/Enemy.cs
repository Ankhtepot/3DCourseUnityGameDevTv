using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] int scoreHitValue = 12;
    [SerializeField] int hits = 3;
    [SerializeField] Vector3 BoxColliderSize;
    [SerializeField] GameObject DeathFX;
    [SerializeField] Transform parent;
    [SerializeField] ScoreBoard scoreBoard;
#pragma warning restore 649


    void Start()
    {
        AddNonTriggerColliderToBody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerColliderToBody()
    {
        var enemyBody = GetComponentInChildren<EnemyBody>();
        var collider = enemyBody.gameObject.AddComponent<BoxCollider>();
        collider.center = Vector3.zero;
        collider.isTrigger = false;
        collider.size = BoxColliderSize;
    }

    public void HandleParticleCollisionWithBody(GameObject other)
    {
        hits -= 1;
        if (hits <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        var fx = Instantiate(DeathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        scoreBoard.ScoreHit(scoreHitValue);
        Destroy(fx, 5000);
        Destroy(gameObject);
    }
}
