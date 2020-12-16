using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float SelfDestructTime = 5f;
    void Start()
    {
        Destroy(gameObject, SelfDestructTime);   
    }    
}
