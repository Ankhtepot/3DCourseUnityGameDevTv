using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] Enemy ParentEnemy;
#pragma warning restore 649
    private void OnParticleCollision(GameObject other)
    {
        ParentEnemy?.HandleParticleCollisionWithBody(other);
    }
}
