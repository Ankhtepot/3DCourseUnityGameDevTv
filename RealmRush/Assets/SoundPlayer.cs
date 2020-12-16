using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class SoundPlayer : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float baseVolume = 0.4f;
#pragma warning restore 649

    public void PlayOnce(AudioClip audioClip, float volume = 0.0f)
    {
        if (Math.Abs(volume) < float.Epsilon)
        {
            volume = baseVolume;
        }
        audioSource.PlayOneShot(audioClip, volume);
    }
}
