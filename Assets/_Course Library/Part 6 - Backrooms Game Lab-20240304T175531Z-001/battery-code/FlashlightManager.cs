using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public enum FlashlightState
{
    Off,
    On,
    Dead
}

[RequireComponent(typeof(AudioSource))]
public class FlashlightManager : MonoBehaviour
{
    [Range(0.0f, 2f)][SerializeField] private float batteryLossTick = 0.5f;
    [SerializeField] private int startBattery = 100;
    public int currentBattery;
    public FlashlightState state;
    private bool flashlightIsOn;

    [SerializeField] private Light flashlightLight; // Adjust this reference to match your actual Light component
    [SerializeField] private GameObject flashlightLightObject; // Reference to the GameObject that holds the Light component
    [SerializeField] private AudioClip flashlightOnFX, flashlightOffFX;
    private bool triggerPressedLastFrame = false;

    // Spotlight angle settings
    [SerializeField] private float maxSpotAngle = 60f; // Maximum angle of the spotlight cone
    [SerializeField] private float minSpotAngle = 30f; // Minimum angle when the battery is low

    void Start()
    {
        currentBattery = startBattery;
        flashlightLight.spotAngle = maxSpotAngle; // Initialize with max spot angle
        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick);
    }

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed);

        if (isPressed && !triggerPressedLastFrame)
        {
            ToggleFlashlight();
        }
        triggerPressedLastFrame = isPressed;

        if (currentBattery <= 0 && state != FlashlightState.Dead)
        {
            TurnOffFlashlight();
        }

        flashlightLightObject.SetActive(state == FlashlightState.On && flashlightIsOn);
    }

    public void GainBattery(int amount)
    {
        if (state == FlashlightState.Dead && amount > 0)
        {
            flashlightIsOn = false; // Ensure flashlight is initially off when battery is regained
            state = FlashlightState.Off;
        }

        currentBattery = Mathf.Clamp(currentBattery + amount, 0, startBattery);
        UpdateSpotlightAngle(); // Update the spotlight angle based on new battery level
    }

    public void LoseBattery()
    {
        if (state == FlashlightState.On && flashlightIsOn)
        {
            currentBattery--;
            UpdateSpotlightAngle();
        }
        if (currentBattery <= 0)
        {
            TurnOffFlashlight();
            state = FlashlightState.Dead;
        }
    }

    private void UpdateSpotlightAngle()
    {
        // Dynamically adjust the spotlight angle based on the battery level
        float percentage = (float)currentBattery / startBattery;
        flashlightLight.spotAngle = Mathf.Lerp(minSpotAngle, maxSpotAngle, percentage);
    }

    private void TurnOffFlashlight()
    {
        flashlightIsOn = false;
        GetComponent<AudioSource>().PlayOneShot(flashlightOffFX);
        flashlightLightObject.SetActive(false);
    }

    public void ToggleFlashlight()
    {
        if (state == FlashlightState.Dead) return;

        flashlightIsOn = !flashlightIsOn;
        state = flashlightIsOn ? FlashlightState.On : FlashlightState.Off;

        if (flashlightIsOn)
        {
            if (flashlightOnFX != null) GetComponent<AudioSource>().PlayOneShot(flashlightOnFX);
        }
        else
        {
            if (flashlightOffFX != null) GetComponent<AudioSource>().PlayOneShot(flashlightOffFX);
        }
    }
}