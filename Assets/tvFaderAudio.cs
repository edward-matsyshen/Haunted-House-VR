using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvFaderAudio : MonoBehaviour
{
    public AudioClip[] audioClips;  // Array of audio clips to play sequentially
    public GameObject targetObject;  // Target GameObject to measure distance from
    public float maxVolume = 1.0f;  // Maximum volume at the closest distance
    public float minVolume = 0.1f;  // Minimum volume at the maximum distance
    public float maxDistance = 10.0f;  // Maximum distance for volume control
    public float fadeOutDuration = 2.0f;  // Duration in seconds for the audio to fade out
    public float delayBetweenClips = 3.0f;  // Delay in seconds between audio clips

    private AudioSource audioSource;  // Reference to the AudioSource component
    private Collider collider;  // Reference to the Collider component
    private bool audioPlayed = false;  // Flag to check if the audio has already been played

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource component attached, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Initialize AudioSource settings
        audioSource.volume = maxVolume;
        audioSource.playOnAwake = false;

        // Get the Collider component attached to the same GameObject
        collider = GetComponent<Collider>();

        // Ensure collider is not null and is set as a trigger
        if (collider == null)
        {
            Debug.LogError("Collider component not found or not set as a trigger.");
        }
        else
        {
            collider.isTrigger = true;
        }
    }

    // OnTriggerEnter is called when another Collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as "Player"
        if (other.CompareTag("Player") && !audioPlayed)
        {
            // Start playing the audio clips if not already played
            StartCoroutine(PlayAudioClips());
            audioPlayed = true;  // Set the flag to true so it doesn't play again
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

    // Coroutine to play audio clips sequentially
    private IEnumerator PlayAudioClips()
    {
        foreach (var clip in audioClips)
        {
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(AdjustVolumeBasedOnDistance());
            yield return new WaitForSeconds(audioSource.clip.length + delayBetweenClips);
        }
    }

    // Coroutine to adjust volume based on distance to the target object
    private IEnumerator AdjustVolumeBasedOnDistance()
    {
        while (audioSource.isPlaying)
        {
            if (targetObject != null)
            {
                float distance = Vector3.Distance(transform.position, targetObject.transform.position);
                audioSource.volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
                audioSource.volume = Mathf.Clamp(audioSource.volume, minVolume, maxVolume);
            }
            yield return null;
        }
    }

    // Coroutine to fade out the audio volume
    private IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = maxVolume;  // Reset the volume
    }
}