using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Battery02 : MonoBehaviour
{
    [SerializeField] private int batteryWeight = 10;
    [SerializeField] private GameObject[] HoverObjects;
    private FlashlightManager flashlightManager;

    private void Start()
    {
        flashlightManager = FindObjectOfType<FlashlightManager>();
    }

    private void Update()
    {
        float distanceToPlayer = PlayerCasting.DistanceFromTarget; 

        bool isPlayerClose = distanceToPlayer < 3.5;
        UpdateHoverObjectsState(isPlayerClose);

        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (isPlayerClose && triggerPressed)
        {
            // Check if this is specifically "battery2"
            if (gameObject.name == "battery2")
            {
                flashlightManager?.GainBattery(batteryWeight);
                Debug.Log("Picking up battery2");
                Destroy(gameObject); // Destroy this specific battery GameObject
                Debug.Log(gameObject);
            }
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