using static Assets.Scripts.strings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 5f;
#pragma warning disable 649
    [SerializeField] GameObject DeathFX;
#pragma warning restore 649
    [Header("Assined on Start")]
    [SerializeField] SceneLoader SL;

    private void Start()
    {
        SL = FindObjectOfType<SceneLoader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        print("Player is dying.");
        DeathFX?.SetActive(true);
        SendMessage(Messages.PLAYER_IS_DYING);
        SL?.ReloadScene(levelLoadDelay);
    }
}
