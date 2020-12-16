using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class GameController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private int BaseBaseHealth = 5;
    [SerializeField] private bool GameStarted;
    [SerializeField] private int unfinishedSpawners;
    [SerializeField] private GameObject WinLabel;
    [SerializeField] private GameObject LoseLabel;
    [SerializeField] private BaseHealthController baseHealthController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip music;
    private List<EnemySpawnerController> spawners = new List<EnemySpawnerController>();
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private void Update()
    {
        if (audioSource && audioSource.isActiveAndEnabled && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// From event
    /// </summary>
    public void OnStartGameRequested()
    {
        if (spawners.Count == 0)
        {
            Debug.LogWarning("No Enemy Spawners found, can't start the game");
            return;
        }

        baseHealthController.Activate();
        baseHealthController.SetHealth(BaseBaseHealth);
        spawners.ForEach(spawner => spawner.Activate(this));
    }

    public void OnSpawnerFinished()
    {
        unfinishedSpawners -= 1;

        if (unfinishedSpawners <= 0)
        {
            StartCoroutine(CheckIfAllEnemiesAreDestroyed());
        }
    }

    private IEnumerator CheckIfAllEnemiesAreDestroyed()
    {
        yield return new WaitForSeconds(0.5f);
        if (FindObjectsOfType<EnemyMovement>().Length <= 0)
        {
            ShowWinCondition();
        }
        else
        {
            StartCoroutine(CheckIfAllEnemiesAreDestroyed());
        }
    }

    public void OnBaseDestroyed()
    {
        ShowLoseCondition();
    }

    private void ShowLoseCondition()
    {
        LoseLabel.SetActive(true);
    }

    private void ShowWinCondition()
    {
        WinLabel.SetActive(true);
    }

    private void initialize()
    {
        spawners = FindObjectsOfType<EnemySpawnerController>().ToList();
        unfinishedSpawners = spawners.Count;
        if (audioSource)
        {
            audioSource.clip = music;
        }
    }

    public void EnemyReachedBase(EnemyInfo info)
    {
        baseHealthController.ReceiveDamage(info ? info : null);
    }
}
