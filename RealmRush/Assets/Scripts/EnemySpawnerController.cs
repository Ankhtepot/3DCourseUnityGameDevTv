using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class EnemySpawnerController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private bool activated;
    [SerializeField] List<float> WavesSpawnTimes = new List<float>();
    [SerializeField] private float defaultWaveSpawnTime = 5f;
    [SerializeField] List<EnemyWave> Waves = new List<EnemyWave>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnYRotation = 0f;
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int currentEnemyInWaveIndex = 0;
    [SerializeField] public UnityEvent OnSpawnerFinished;
#pragma warning restore 649

    private void Update()
    {
        if (activated)
        {
            currentWaveIndex = 0;
            currentEnemyInWaveIndex = 0;
            activated = false;
            if (Waves.Count > 0 && currentWaveIndex < Waves.Count)
            {
                StartCoroutine(SpawnWave());
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        if (Waves.Count > currentWaveIndex)
        {
            var waveStartDelay = currentWaveIndex < WavesSpawnTimes.Count
                ? WavesSpawnTimes[currentWaveIndex]
                : defaultWaveSpawnTime;
            
            yield return new WaitForSeconds(waveStartDelay);
        
            StartCoroutine(SpawnEnemies(Waves[currentWaveIndex]));
        }
        else
        {
            OnSpawnerFinished?.Invoke();
        }
        
    }

    private IEnumerator SpawnEnemies(EnemyWave newWave)
    {
        currentEnemyInWaveIndex = 0;
        var spawnDelay = newWave.GetSpawnDelay();
        
        while (currentEnemyInWaveIndex < newWave.WaveSize)
        {
            var newEnemy = Instantiate(newWave.GetEnemyOnIndex(currentEnemyInWaveIndex++), spawnPoint.transform.position, Quaternion.Euler(new Vector3(0, spawnYRotation, 0)));
            newEnemy.transform.parent = gameObject.transform;
            
            yield return new WaitForSeconds(spawnDelay);
        }

        currentWaveIndex++;

        StartCoroutine(SpawnWave());
    }

    public Vector2Int GetSpawnPointPosition()
    {
        return spawnPoint.transform.position.ToGridVector2Int();
    }

    public void Activate(GameController reportTo)
    {
        activated = true;
        OnSpawnerFinished.AddListener(reportTo.OnSpawnerFinished);
    }
}
