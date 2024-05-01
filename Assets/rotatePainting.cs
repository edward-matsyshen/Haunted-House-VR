using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class rotatePainting : MonoBehaviour
{
    public Animation doorAnimation; // Reference to the door's Animation component
    public string animationName = "doorTrigPad"; // Name of the animation to play
    public rotatePainting[] allPaintings; // References to other painting scripts
    public float rotationAngle = 90f; // Angle to rotate the painting
    private bool isRotated = false; // To prevent multiple triggers

    void Update() // Use Update to check for VR input
    {
        Debug.Log("Checking for right-hand controller");

        // Get the right-hand controller
        var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightHandDevice.isValid) // Ensure the controller is valid
        {
            Debug.Log("Right-hand controller is valid");

            // Check if "A" button (primary button) is pressed
            if (rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {
                Debug.Log("Primary button is pressed"); // Confirmation of input detection

                if (!isRotated) // Avoid multiple rotations
                {
                    transform.Rotate(0, 0, rotationAngle); // Rotate painting by 90 degrees

                    if (Mathf.Approximately(transform.eulerAngles.z, 0f)) // Check if Z = 0
                    {
                        isRotated = true; // Avoid further rotations

                        if (AllPaintingsAtZero()) // If all paintings are at Z = 0
                        {
                            Debug.Log("Triggering door animation");
                            doorAnimation.Play(animationName); // Trigger the door animation
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Right-hand controller not found or not valid");
        }
    }


    private bool AllPaintingsAtZero()
    {
        foreach (var painting in allPaintings)
        {
            if (!Mathf.Approximately(painting.transform.eulerAngles.z, 0f)) // Check if any painting is not at Z = 0
            {
                return false; // Return false if even one painting is not at 0
            }
        }
        return true; // All paintings are at Z = 0
    }
}
