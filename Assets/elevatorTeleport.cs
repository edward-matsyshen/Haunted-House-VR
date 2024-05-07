using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class elevatorTeleport : MonoBehaviour
{
    public Transform targetTeleportLocation;
    public Transform teleportOriginObject;
    private Transform playerRelativeLocation;
    public float teleportDelay = 0.0f;
    private ActionBasedContinuousMoveProvider moveProvider;
    // Reference to the directional light
    private bool isTeleporting = false;  // To prevent multiple teleportations simultaneously

    public AudioClip audioClip;  // The audio clip to play
    private AudioSource audioSource;  // Reference to the AudioSource component

    public Collider doorTrigger; // The trigger to open the doors in the basement

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource component attached, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource component
        audioSource.clip = audioClip;

        //disable the basement elevator doors
        doorTrigger.enabled = false;
    }

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
            audioSource.Play();
            StartCoroutine(TeleportAfterDelay(other.gameObject));
            doorTrigger.enabled = true;
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
        player.transform.position = targetTeleportLocation.position + (player.transform.position - teleportOriginObject.transform.position);

        // Re-enable movement
        moveProvider.enabled = true;
        isTeleporting = false;
    }

    private IEnumerator WaitForTenSeconds()
    {
        Debug.Log("Coroutine started");
        
        yield return new WaitForSeconds(18f); // Wait for 10 seconds
        
        Debug.Log("18 seconds have passed");
        
        // Add your code here to execute after waiting for ten seconds
    }
}
