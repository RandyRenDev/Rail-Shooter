using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xPos = 10f;
    [Tooltip("In ms^-1")] [SerializeField] float yPos = 10f;
    [SerializeField] int damage;
    [SerializeField] GameObject[] Guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw based")]
    [SerializeField] float controllRollFactor = -3f;
    [SerializeField] float controlPitchFactor = -5f;

    float xThrow;
    float yThrow;

    bool isControlEnabled = true;

    

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessMovement();
            ProcessRoll();
            ProcessFiring();
        }
  
    }

    // called by string reference in CollisionHandler
    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }

    void ProcessRoll()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controllRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;

        float clampedXPos = Mathf.Clamp(rawNewXPos, -xPos, xPos);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;

        float clampedYPos = Mathf.Clamp(rawNewYPos, -yPos, yPos);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactivateGuns();
        }
    }

    void ActivateGuns()
    {
        foreach(GameObject gun in Guns)
        {
            gun.SetActive(true);
        }
    }

    void DeactivateGuns()
    {
        foreach(GameObject gun in Guns)
        {
            gun.SetActive(false);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
