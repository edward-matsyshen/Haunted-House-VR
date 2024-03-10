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

    private bool isPlayerClose = false;

    private void Update()
    {
        UpdateHoverObjectsState();

        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (isPlayerClose && triggerPressed)
        {
            FindObjectOfType<FlashlightManager>()?.GainBattery(batteryWeight);
            Destroy(gameObject);
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

    private void UpdateHoverObjectsState()
    {
        foreach (GameObject hoverObject in HoverObjects)
        {
            hoverObject.SetActive(isPlayerClose);
        }
    }
}