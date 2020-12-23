using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class BaterryPickup : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float angleRestore = 20f;
    [SerializeField] private float intensityRestore = 5f;

    private FlashlightSystem flashlight;
#pragma warning restore 649

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            flashlight = other.GetComponentInChildren<FlashlightSystem>();
            Debug.Log($"BaterryPickup triggered");
            flashlight.RestoreLightAngle(angleRestore);
            flashlight.RestoreLightIntensity(intensityRestore);    
            Destroy(gameObject);
        }
        
    }
}
