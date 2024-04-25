using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class rotateObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 45f; // Speed of rotation in degrees per second
    [SerializeField] private GameObject[] hoverObjects; // Objects to display when interaction is possible

    private bool isPlayerClose = false; // Flag to track if the player is nearby



    private void Update()
    {
        UpdateHoverObjectsState();

        // Check if the player is close and presses the trigger button
        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (isPlayerClose && triggerPressed)
        {
            RotateObject(); // Rotate the object when the trigger is pressed
        }
    }

    private void RotateObject()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player has the "Player" tag
        {
            isPlayerClose = true; // Set flag when the player is close
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Reset flag when the player leaves
        {
            isPlayerClose = false;
        }
    }

    private void UpdateHoverObjectsState()
    {
        foreach (GameObject hoverObject in hoverObjects)
        {
            hoverObject.SetActive(isPlayerClose); // Display hover objects when player is close
        }
    }
}