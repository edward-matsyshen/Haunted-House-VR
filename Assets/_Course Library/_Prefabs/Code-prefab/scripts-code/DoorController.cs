using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animation doorAnimation;
    public bool lockedByPassword = true; // Ensure this is true by default
    public delegate void DoorEvent();
    public event DoorEvent OnDoorUnlocked; // Event to trigger when the door is unlocked

    private void Update()
    {
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
            GetComponent<BoxCollider>().enabled = false;
            doorAnimation.Play();

            OnDoorUnlocked?.Invoke(); // Invoke the event
        }
        else
        {
            Debug.Log("Door is still locked.");
        }
    }
}
