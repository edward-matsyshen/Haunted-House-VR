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

    [SerializeField] private Light flashlightLight;
    [SerializeField] private GameObject flashlightLightObject;
    [SerializeField] private AudioClip flashlightOnFX, flashlightOffFX;
    private bool triggerPressedLastFrame = false;

    [SerializeField] private float maxSpotAngle = 60f;
    [SerializeField] private float minSpotAngle = 30f;

    private float randomOffTimer;
    private float nextRandomOffTime;

    void Start()
    {
        currentBattery = startBattery;
        flashlightLight.spotAngle = maxSpotAngle;
        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick);
        SetNextRandomOffTime();
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

        if (Time.time > nextRandomOffTime && flashlightIsOn)
        {
            StartCoroutine(RandomlyTurnOff());
        }
    }

    private void SetNextRandomOffTime()
    {
        nextRandomOffTime = Time.time + 300f + Random.Range(0f, 120f); // 5 minutes plus a random extra time
    }

    private IEnumerator RandomlyTurnOff()
    {
        TurnOffFlashlight();
        yield return new WaitForSeconds(Random.Range(3f, 7f)); // Turn off for a random time between 2 to 5 seconds
        if (currentBattery > 0 && state != FlashlightState.Dead)
        {
            ToggleFlashlight(); // Turn it back on if still possible
        }
        SetNextRandomOffTime(); // Reset the timer for next random off
    }

    public void GainBattery(int amount)
    {
        if (state == FlashlightState.Dead && amount > 0)
        {
            flashlightIsOn = false;
            state = FlashlightState.Off;
        }

        currentBattery = Mathf.Clamp(currentBattery + amount, 0, startBattery);
        UpdateSpotlightAngle();
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