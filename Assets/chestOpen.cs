using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class chestOpen : MonoBehaviour
{
    [Header("Attributes")]
    public List<string> requiredKeys = new List<string>(); // List of key names required to trigger the chest

    [Header("References")]
    public Animation chestAnimation;
    public AudioSource chestOpenSound;
    public AudioSource chestLockedSound;
    public GameObject HoverIconWithoutKey; // Hover object when the key is not in inventory
    public GameObject HoverIconWithKey;   // Hover object when the key is in inventory

    private KeyManager keyManager; // Reference to the KeyManager component
    private bool isChestOpen = false; // Track whether the chest is open
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
            UpdateHoverObjects(); // Update hover objects based on player's key possession
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = false; // Reset flag when the player leaves the chest area
            UpdateHoverObjects(); // Ensure hover objects are inactive
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
            if (requiredKeys.Exists(rk => rk.Trim().ToLower() == key.Trim().ToLower()))
            {
                OpenChest();
                keyToRemove = key; // Remember the key to remove after opening
                break;
            }
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
        chestAnimation.Play(); // Play chest opening animation
        chestOpenSound.Play(); // Play chest opening sound
        isChestOpen = true; // Mark the chest as open
        UpdateHoverObjects(); // Ensure hover objects are deactivated after opening
    }

    private void UpdateHoverObjects()
    {
        if (playerNearChest && !isChestOpen)
        {
            bool hasRequiredKey = keyManager.keysInInventory.Exists(k => requiredKeys.Exists(rk => rk.Trim().ToLower() == k.Trim().ToLower()));

            // Set the appropriate hover object based on whether the player has the key
            HoverIconWithoutKey.SetActive(!hasRequiredKey);
            HoverIconWithKey.SetActive(hasRequiredKey);
        }
        else
        {
            // Ensure both hover objects are inactive when the chest is open
            HoverIconWithoutKey.SetActive(false);
            HoverIconWithKey.SetActive(false);
        }
    }
}