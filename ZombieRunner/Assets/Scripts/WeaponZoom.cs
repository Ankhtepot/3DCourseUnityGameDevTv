using System;
using System.Collections;
using System.Collections.Generic;
using ECM.Components;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class WeaponZoom : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxZoom = 45f;
    [SerializeField] private float zoomInSpeed = 0.2f;
    [SerializeField] private float zoomOutSpeed = 1f;
    [SerializeField] private float mouseSensitivityZoomedIn = 1f;
    
    private float mouseSensitivityZoomedOut;
    private float baseFieldOfView = 90f;
    private MouseLook mouseLook;
#pragma warning restore 649

    private void Start()
    {
        baseFieldOfView = mainCamera.fieldOfView;
        mouseLook = FindObjectOfType<MouseLook>();
        mouseSensitivityZoomedOut = mouseLook.lateralSensitivity;
    }

    void Update()
    {
        ManageInput();   
    }
    
    private void ManageInput()
    {
        if(Input.GetButton("Fire2") )
        {
            if (mainCamera.fieldOfView > maxZoom)
            {
                mainCamera.fieldOfView -= zoomInSpeed;
                mouseLook.lateralSensitivity = mouseSensitivityZoomedIn;
                mouseLook.verticalSensitivity = mouseSensitivityZoomedIn;
            }
        }
        else if (mainCamera.fieldOfView < baseFieldOfView)
        {
            mainCamera.fieldOfView += zoomOutSpeed;
            mouseLook.lateralSensitivity = mouseSensitivityZoomedOut;
            mouseLook.verticalSensitivity = mouseSensitivityZoomedOut;
        }
    }
}
