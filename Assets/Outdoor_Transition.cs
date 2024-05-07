using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Outdoor_Transition : MonoBehaviour
{
    public Transform targetTeleportLocation;
    public float teleportDelay = 0.0f;
    private ActionBasedContinuousMoveProvider moveProvider;
    // Reference to the directional light
    private bool isTeleporting = false;  // To prevent multiple teleportations simultaneously

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            moveProvider = other.GetComponentInParent<ActionBasedContinuousMoveProvider>(); // Assuming the XR Rig is a parent of the collider object
            if (moveProvider == null)
            {
                Debug.LogWarning("MoveProvider not found on the player.");
                return; // Early exit if no MoveProvider found
            }

            StartCoroutine(TeleportAfterDelay(other.gameObject));
        }
    }

    private IEnumerator TeleportAfterDelay(GameObject player)
    {
        isTeleporting = true;
        // Disable movement
        moveProvider.enabled = false;

        // Wait for the specified delay
        yield return new WaitForSeconds(teleportDelay);

        // Teleport the player to the target location and adjust rotation to match target
        player.transform.position = targetTeleportLocation.position;
        player.transform.rotation = targetTeleportLocation.rotation;

        // Re-enable movement
        moveProvider.enabled = true;
        isTeleporting = false;
    }
}
