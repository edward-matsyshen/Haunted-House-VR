using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTeleport : MonoBehaviour
{
    public Transform teleportDestination; // Assign the destination point in the inspector

    void Start()
    {
        EnsureTriggerCollider();
    }

    // Ensure there is a trigger collider on the GameObject
    void EnsureTriggerCollider()
    {
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            // Add a box collider if none exists
            collider = gameObject.AddComponent<BoxCollider>();
        }
        // Set the collider as a trigger
        collider.isTrigger = true;
    }

    // Called when any collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the destination
            TeleportPlayerToDestination(other.transform);
        }
    }

    // Teleport and rotate the player to match the destination transform
    void TeleportPlayerToDestination(Transform player)
    {
        player.position = teleportDestination.position; // Teleport the player to the destination
        player.rotation = teleportDestination.rotation; // Adjust player's rotation to match the destination
    }
}