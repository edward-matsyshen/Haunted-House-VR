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

    [SerializeField] private GameObject FlashlightLight;
    [SerializeField] private AudioClip FlashlightOn_FX, FlashlightOff_FX;
    private bool triggerPressedLastFrame = false;

    void Start()
    {
        currentBattery = startBattery;
        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick);
    }

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out bool isPressed);

        // Toggle flashlight on trigger press, ensuring it only toggles when the press state changes
        if (isPressed && !triggerPressedLastFrame)
        {
            ToggleFlashlight();
        }
        triggerPressedLastFrame = isPressed;

        if (currentBattery <= 0 && state != FlashlightState.Dead)
        {
            TurnOffFlashlight();
        }

        // Update flashlight state visually and functionally
        FlashlightLight.SetActive(state == FlashlightState.On && flashlightIsOn);
    }

    public void GainBattery(int amount)
    {
        if (state == FlashlightState.Dead && amount > 0)
        {
            flashlightIsOn = false; // Ensure flashlight is initially off when battery is regained
            state = FlashlightState.Off;
        }

        currentBattery = Mathf.Clamp(currentBattery + amount, 0, startBattery);
    }

    public void LoseBattery()
    {
        if (state == FlashlightState.On && flashlightIsOn) currentBattery--;
        if (currentBattery <= 0)
        {
            TurnOffFlashlight();
            state = FlashlightState.Dead;
        }
    }

    private void TurnOffFlashlight()
    {
        flashlightIsOn = false;
        GetComponent<AudioSource>().PlayOneShot(FlashlightOff_FX);
        FlashlightLight.SetActive(false);
    }

    public void ToggleFlashlight()
    {
        if (state == FlashlightState.Dead) return; // Do not toggle if the flashlight is dead

        flashlightIsOn = !flashlightIsOn;
        state = flashlightIsOn ? FlashlightState.On : FlashlightState.Off;

        if (flashlightIsOn)
        {
            if (FlashlightOn_FX != null) GetComponent<AudioSource>().PlayOneShot(FlashlightOn_FX);
        }
        else
        {
            if (FlashlightOff_FX != null) GetComponent<AudioSource>().PlayOneShot(FlashlightOff_FX);
        }
    }
}