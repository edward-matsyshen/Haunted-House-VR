using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ClickedOnLeftTrig : MonoBehaviour
{
    public GameObject directionalLight; // The directional light to be toggled
    public GameObject otherLight; // The other light source to be toggled
    public GameObject extraLight; // The other light source to be toggled
    public GameObject hoverIcon; // Optional hover icon
    private bool isPlayerClose = false; // Tracks if the player is close to the light switch
    private bool lightsAreOn = false; // Tracks the state of the lights

    void Update()
    {
        UpdateHoverIconState();

        // Check if the left-hand trigger button is pressed
        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed && isPlayerClose)
        {
            ToggleLights(); // Toggle the lights when the trigger is pressed and the player is close
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player GameObject has a "Player" tag
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }

    void ToggleLights()
    {
        // Toggle the lights based on their current state
        lightsAreOn = !lightsAreOn;

        directionalLight.SetActive(lightsAreOn); // Toggle the directional light
        otherLight.SetActive(lightsAreOn); // Toggle the other light source
        extraLight.SetActive(lightsAreOn); // Toggle the other light source
    }

    void UpdateHoverIconState()
    {
        if (hoverIcon != null) // If a hover icon is provided
        {
            hoverIcon.SetActive(isPlayerClose); // Display the hover icon when the player is close
        }
    }
}

