using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class LandingPad : MonoBehaviour
{
#pragma warning disable 649
    [NotNull (IgnorePrefab = true)] [SerializeField] private RocketShip Player;
    [NotNull] [SerializeField]private ParticleSystem finishVFX;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    void Update()
    {
        
    }

    private void onReachedFinish()
    {
        finishVFX.Play();
    }
    
    private void initialize()
    {
        Player = FindObjectOfType<RocketShip>();
        Player.ReachedFinish.AddListener(onReachedFinish);
    }
}
