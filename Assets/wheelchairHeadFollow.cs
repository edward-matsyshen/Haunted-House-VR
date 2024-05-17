using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelchairHeadFollow : MonoBehaviour
{
    public Transform cameraTransform; // Assign this to your VR headset's transform in the inspector.
    private float previousYRotation;

    void Start()
    {
        // Initialize previousYRotation with the initial y-axis rotation of the camera.
        previousYRotation = cameraTransform.eulerAngles.y;
    }

    void Update()
    {
        // Current y-axis rotation of the camera
        float currentYRotation = cameraTransform.eulerAngles.y;

        // Check if there is a change in the y-axis rotation
        if (currentYRotation != previousYRotation)
        {
            // Apply the new rotation to the wheelchair, matching the camera's y-axis rotation
            transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
            // Update previousYRotation for the next frame
            previousYRotation = currentYRotation;
        }
    }
}