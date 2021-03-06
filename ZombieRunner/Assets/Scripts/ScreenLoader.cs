﻿using ECM.Components;
using ECM.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

//Fireball Games * * * PetrZavodny.com

public class ScreenLoader : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private BaseCharacterController playerController;
    [SerializeField] private int currentSceneIndex;
#pragma warning restore 649

    private void Start()
    {
        // playerController = FindObjectOfType<BaseCharacterController>();
        // InitializeMouse();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
    }

    private void InitializeMouse()
    {
        playerController.pause = false;
        var mouseLook = playerController.GetComponent<MouseLook>();
        mouseLook.SetCursorLock(true);
        mouseLook.verticalSensitivity = 2;
        mouseLook.lateralSensitivity = 2;
    }

    public void OnPlayAgainClick()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnQuitClick()
    {
        print("Application quits.");
        Application.Quit();
    }

    private void OnSceneLoaded()
    {
        
    }
}
