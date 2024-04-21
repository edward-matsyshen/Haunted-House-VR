using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class chestOpen : MonoBehaviour
{
    [Header("Attributes")]
    public List<string> requiredKeys = new List<string>();  // List of key names required to trigger the chest

    [Header("References")]
    public Animation chestAnimation;
    public AudioSource chestOpenSound;
    public AudioSource chestLockedSound;

    private KeyManager keyManager; // Reference to the KeyManager component
    private bool isChestOpen = false; // Track whether the chest has been opened
    private bool playerNearChest = false; // Detect if player is near the chest

    private void Start()
    {
        keyManager = FindObjectOfType<KeyManager>(); // Find the KeyManager component in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = true; // Set flag to true when the player is near the chest
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = false; // Reset flag when the player leaves the chest area
        }
    }

    private void Update()
    {
        if (playerNearChest && !isChestOpen)
        {
            if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {
                CheckForRequiredKey();
            }
        }
    }

    private void CheckForRequiredKey()
    {
        string keyToRemove = null;
        foreach (string key in keyManager.keysInInventory)
        {
            foreach (string requiredKey in requiredKeys)
            {
                if (key.Trim().ToLower() == requiredKey.Trim().ToLower())
                {
                    OpenChest();
                    keyToRemove = key; // Remember the key to remove after opening
                    break;
                }
            }
            if (keyToRemove != null)
                break; // Break if the key is found
        }

        if (keyToRemove != null)
        {
            keyManager.keysInInventory.Remove(keyToRemove); // Remove the key from inventory
        }
        else
        {
            chestLockedSound.Play(); // Play the locked chest sound if no key matches
        }
    }

    private void OpenChest()
    {
        GetComponent<BoxCollider>().enabled = false; // Disable collider to prevent retriggering
        chestAnimation.Play(); // Trigger chest opening animation
        chestOpenSound.Play(); // Play the chest opening sound
        isChestOpen = true; // Mark the chest as open
    }
}