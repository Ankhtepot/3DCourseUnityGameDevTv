using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

[CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave")]
public class EnemyWave : ScriptableObject
{
#pragma warning disable 649
    [SerializeField] float spawnDelay;
    [SerializeField] private List<EnemyMovement> enemies = new List<EnemyMovement>();
    public int WaveSize => enemies.Count;
#pragma warning restore 649

    public float GetSpawnDelay() => spawnDelay;

    public EnemyMovement GetEnemyOnIndex(int enemyIndex)
    {
        if (enemyIndex < enemies.Count)
        {
            return enemies[enemyIndex];
        }

        return null;
    }
}
