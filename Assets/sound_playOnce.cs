using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioAndDisableCollider : MonoBehaviour
{
    public AudioClip audioClip;  // The audio clip to play
    public float volume = 1.0f;  // Volume level of the audio clip (0.0f to 1.0f)
    
    private AudioSource audioSource;  // Reference to the AudioSource component
    private Collider collider;  // Reference to the Collider component

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
        audioSource.volume = volume;  // Set the initial volume

        // Get the Collider component attached to the same GameObject
        collider = GetComponent<Collider>();

        // Ensure collider is not null and is set as a trigger
        if (collider == null)
        {
            Debug.LogError("Collider component not found or not set as trigger.");
        }
        else
        {
            collider.isTrigger = true;  // Ensure collider is set as a trigger
        }
    }

    // OnTriggerEnter is called when another Collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Check if the audio clip is assigned and not currently playing
            if (audioClip != null && !audioSource.isPlaying)
            {
                // Play the audio clip
                audioSource.Play();

                // Set the volume of the audio clip
                audioSource.volume = volume;

                // Disable the collider to prevent further triggers
                collider.enabled = false;
            }
        }
    }
}
