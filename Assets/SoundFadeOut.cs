using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeOut : MonoBehaviour
{
    public AudioClip audioClip;  // The audio clip to play
    public float volume = 1.0f;  // Volume level of the audio clip (0.0f to 1.0f)
    public float fadeOutDuration = 2.0f;  // Duration in seconds for the audio to fade out

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
            // Play the audio clip if not already playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            // Set the volume back to full in case it was fading out
            audioSource.volume = volume;
        }
    }

    // OnTriggerExit is called when another Collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Start the fade out coroutine
            StartCoroutine(FadeOutAudio());
        }
    }

    // Coroutine to fade out the audio volume
    private IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;

        // Gradually decrease the volume
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;  // Wait for the next frame
        }

        // Stop the audio after fading out
        audioSource.Stop();
        audioSource.volume = volume;  // Reset the volume to its initial value
    }
}
