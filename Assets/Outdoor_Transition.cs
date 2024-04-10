using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Outdoor_Transition : MonoBehaviour
{
    public Transform targetTeleportLocation;
    public float teleportDelay = 2.0f;
    private ActionBasedContinuousMoveProvider moveProvider;
    // Reference to the directional light
    public Light directionalLight;

   private void OnTriggerEnter(Collider other)
   {
        
        if (other.CompareTag("Player"))
        {
            
            moveProvider = other.GetComponentInParent<ActionBasedContinuousMoveProvider>(); // Assuming the XR Rig is a parent of the collider object
            StartCoroutine(TeleportAfterDelay(other.gameObject));
            if (directionalLight != null)
            {
                // Turn off the directional light
                directionalLight.enabled = false;
                Debug.Log("Directional light turned off.");
            }
            else
            {
                Debug.LogWarning("No directional light found in the scene.");
            }
        }
            
   }

   private IEnumerator TeleportAfterDelay(GameObject player)
        {
            // Disable movement
            if (moveProvider != null) moveProvider.enabled = false;

            // Instantiate the effect prefab at the teleport location, facing the player's forward direction

            // Play the teleport sound effect

            // Wait for the specified delay
            yield return new WaitForSeconds(teleportDelay);

            // Teleport the player to the target location and adjust rotation to match target
            player.transform.position = targetTeleportLocation.position;
            player.transform.rotation = targetTeleportLocation.rotation;

            // Optionally, if you want to apply a fade effect, call the fade method here
            // Make sure the fade in completes before moving the player and fade out after the move

            // Re-enable movement
            if (moveProvider != null) moveProvider.enabled = true;
        }
}
