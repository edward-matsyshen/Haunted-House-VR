using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animation doorAnimation;
    public bool lockedByPassword = true; // Ensure this is true by default

    private void Update()
    {
        // Attempt to open the door if the A button is pressed and the door is unlocked
        AttemptToUnlock();
    }

    private void AttemptToUnlock()
    {
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
        {
            if (!lockedByPassword)
            {
                OpenUp();
            }
        }
    }

    public void OpenUp()
    {
        Debug.Log("Attempting to open the door.");

        if (!lockedByPassword)
        {
            Debug.Log("Door is unlocked, opening now.");
            GetComponent<BoxCollider>().enabled = false; // Optional: Depends on your game's logic
            doorAnimation.Play(); // Play the door opening animation
        }
        else
        {
            Debug.Log("Door is still locked.");
        }
    }
}