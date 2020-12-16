using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class WeaponZoom : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Camera mainCamera;
#pragma warning restore 649

    void Update()
    {
        ManageInput();   
    }
    
    private void ManageInput()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            
        }
    }
}
