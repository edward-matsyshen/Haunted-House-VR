using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class rotateRoom : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 45f; // Speed of rotation in degrees per second

    private void Update()
    {
        var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed)
        {
            RotateObject(); // Rotate the object when the trigger is pressed
        }
    }

    private void RotateObject()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}