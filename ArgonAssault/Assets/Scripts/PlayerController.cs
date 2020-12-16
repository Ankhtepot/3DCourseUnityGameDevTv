using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using static Assets.Scripts.strings;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float MovementSpeedX = 20f;
    [SerializeField] private float MovementSpeedY = 20f;
    [Range(1f, 10f)] [SerializeField] private float MaxMovementValueX = 4;
    [Range(1f, 10f)] [SerializeField] private float MaxMovementValueY = 6;
    [SerializeField] GameObject body;
    [SerializeField] List<GameObject> Guns;
    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 3f;
    [Header("Controll-throw Based")]
    [SerializeField] float controlRollFactor = 3f;
    [SerializeField] float controlPitchFactor = -3f;
    [Header("Statuses")]
    [SerializeField] bool isControlEnabled = true;

    private float xThrow;
    private float yThrow;

    void Start()
    {

    }

    void Update()
    {
        ProcessMovement();
        ProcessFiring();
    }

    private void ProcessMovement()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        float horizontalThrow = CrossPlatformInputManager.GetAxis(Axis.HORIZONTAL);
        float verticalThrow = CrossPlatformInputManager.GetAxis(Axis.VERTICAL);

        if (!Mathf.Approximately(horizontalThrow, Mathf.Epsilon))
        {
            xThrow = horizontalThrow * MovementSpeedX * Time.deltaTime;
            float rawNewXPos = Mathf.Clamp(transform.localPosition.x + xThrow, -MaxMovementValueX, MaxMovementValueX);
            transform.localPosition = new Vector3(rawNewXPos, transform.localPosition.y, transform.localPosition.z);
        }

        if (!Mathf.Approximately(verticalThrow, Mathf.Epsilon))
        {
            yThrow = verticalThrow * MovementSpeedY * Time.deltaTime;
            float rawNewYPos = Mathf.Clamp(transform.localPosition.y + yThrow, -MaxMovementValueY, MaxMovementValueY);
            transform.localPosition = new Vector3(transform.localPosition.x, rawNewYPos, transform.localPosition.z);
        }
    }

    public void PlayerIsDying() // called by string reference
    {
        print("PlayerController received message: " + Messages.PLAYER_IS_DYING);
        isControlEnabled = false;
        EnableBody(false);
    }

    private void ProcessFiring()
    {
            EnableGuns(CrossPlatformInputManager.GetButton(Buttons.FIRE));
    }

    private void EnableGuns(bool isEnabled)
    {
        Guns?.ForEach(gun =>
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isEnabled;
        });
    }

    private void EnableBody(bool enabled)
    {
        body?.SetActive(enabled);
    }
}
