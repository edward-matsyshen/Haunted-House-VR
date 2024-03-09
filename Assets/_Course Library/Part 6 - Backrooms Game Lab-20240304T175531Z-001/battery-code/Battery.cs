using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Battery : MonoBehaviour
{
    [SerializeField] private int batteryWeight = 10;
    [SerializeField] private GameObject[] HoverObjects;
    private FlashlightManager flashlightManager;

    private void Start()
    {
        // Cache the FlashlightManager instance at start to avoid repeated FindObjectOfType calls in Update
        flashlightManager = FindObjectOfType<FlashlightManager>();
    }

    private void Update()
    {
        // Simulate proximity detection using PlayerCasting or a similar approach
        float distanceToPlayer = PlayerCasting.DistanceFromTarget; // Assuming PlayerCasting.DistanceFromTarget takes a Vector3 argument for the target position

        bool isPlayerClose = distanceToPlayer < 4;
        UpdateHoverObjectsState(isPlayerClose);

        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (isPlayerClose && triggerPressed)
        {
            flashlightManager?.GainBattery(batteryWeight);
            Destroy(gameObject);
        }
    }

    private void UpdateHoverObjectsState(bool isPlayerClose)
    {
        foreach (GameObject hoverObject in HoverObjects)
        {
            hoverObject.SetActive(isPlayerClose);
        }
    }
}
