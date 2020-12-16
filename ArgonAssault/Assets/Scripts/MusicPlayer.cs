using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            this.gameObject.SetActive(false);
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        initialize();
    }

    private void initialize()
    {
        
    }
}
