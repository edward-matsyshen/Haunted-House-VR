using UnityEngine.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorManager : MonoBehaviour // This script should be on the Door Trigger
{
    public Animation Door;
    public AudioSource DoorOpenSound;
    private bool isDoorOpen = false; // To prevent the door from being repeatedly triggered

    private void OnTriggerStay(Collider other)
    {
        // Check if the collider is tagged as "Player" and the door is not already open
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            // Check for grip button press on either hand
            if (IsGripPressed(XRNode.LeftHand) || IsGripPressed(XRNode.RightHand))
            {
                OpenDoor();
            }
        }
    }

    private bool IsGripPressed(XRNode hand)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(hand);
        return device.TryGetFeatureValue(CommonUsages.gripButton, out bool isPressed) && isPressed;
    }

    void OpenDoor()
    {
        GetComponent<BoxCollider>().enabled = false; // Optionally disable the collider to prevent re-triggering
        Door.Play(); // Play the door open animation
        DoorOpenSound.Play(); // Play the door open sound
        isDoorOpen = true; // Mark the door as open
    }
}