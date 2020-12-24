using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Fireball Games * * * PetrZavodny.com

public class DisplayDamage : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Image damageImage;
    [SerializeField] private float decaySpeed = 20f;
    [SerializeField] private float flowDownSpeed = 2f;
    [SerializeField] private float fullScale = 1;
    [SerializeField] private float minimalScale = 0.2f;
    [SerializeField] private float currentScale;
#pragma warning restore 649

    void Start()
    {
        initialize();
    }

    private void TriggerDamageDisplay()
    {
        damageImage.gameObject.SetActive(true);
        currentScale = fullScale;
        damageImage.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (currentScale > minimalScale)
        {
            currentScale -= decaySpeed * Time.deltaTime;
            SetDamageImageScale(currentScale);
            FlowDown();
        }
        else 
        {
            damageImage.gameObject.SetActive(false);
        }
    }

    private void SetDamageImageScale(float dimension)
    {
        damageImage.transform.localScale = new Vector3(dimension, dimension, dimension);
    }

    private void FlowDown()
    {
        var flowDownVector = new Vector3(0, flowDownSpeed, 0);
        damageImage.transform.position -= flowDownVector;
    }

    private void initialize()
    {
        currentScale = damageImage.transform.localScale.x;
        FindObjectOfType<PlayerHealth>().OnPlayerDamaged += TriggerDamageDisplay;
    }
}
