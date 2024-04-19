using UnityEngine.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class MBKeyDoor : MonoBehaviour
{
    [Header("Attributes")]
    public List<string> keyNames = new List<string>();  // Change to a list of keys

    [Header("References")]
    public Animation Door;
    public AudioSource DoorOpenSound;
    public AudioSource LockedDoorSound;

    private KeyManager km;
    private bool isDoorOpen = false; // To prevent the door from being repeatedly triggered
    private bool playerInTrigger = false; // Flag to check if player is in trigger

    private void Start()
    {
        km = FindObjectOfType<KeyManager>(); // Ensure there's a KeyManager component in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true; // Set flag true when player enters trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; // Reset flag when player leaves trigger
        }
    }

    private void Update()
    {
        if (playerInTrigger && !isDoorOpen) // Check if player is in trigger and door isn't already open
        {
            // Check for A button press on the right-hand controller
            if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {
                CheckForKeyAndOpenDoor();
            }
        }
    }

    private void CheckForKeyAndOpenDoor()
    {
        string keyToRemove = null;
        foreach (string key in km.keysInInventory)
        {
            // Check if any key in inventory matches any of the allowed key names for this door
            foreach (string allowedKey in keyNames)
            {
                if (key.Trim().ToLower() == allowedKey.Trim().ToLower())
                {
                    OpenDoor();
                    keyToRemove = key; // Mark the key for removal
                    break; // Exit the inner loop once the key is found
                }
            }
            if (keyToRemove != null)
                break; // Exit the outer loop if a key was marked for removal
        }

        if (keyToRemove != null)
        {
            km.keysInInventory.Remove(keyToRemove); // Removes the key from the inventory
        }
        else
        {
            LockedDoorSound.Play(); // Play locked door sound if no key found
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