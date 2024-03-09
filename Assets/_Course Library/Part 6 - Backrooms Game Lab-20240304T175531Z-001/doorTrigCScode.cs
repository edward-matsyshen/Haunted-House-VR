using UnityEngine.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorManager : MonoBehaviour // This script should be on the Door Trigger
{
    public Animation Door;
    public AudioSource DoorOpenSound;
    private bool isDoorOpen = false; // To prevent the door from being repeatedly triggered

    void Update()
    {
        // Check if the player is close enough to the door and the door is not already open
        if (PlayerCasting.DistanceFromTarget < 4 && !isDoorOpen) 
        {
            // Check for A button press on the right-hand controller
            if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        GetComponent<BoxCollider>().enabled = false; // Optionally disable the collider to prevent re-triggering
        Door.Play(); // Play the door open animation
        DoorOpenSound.Play(); // Play the door open sound
        isDoorOpen = true; // Mark the door as open
    }
}