using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onLightBoxSFX : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip to play
    public float volume = 1.0f; // Volume level of the audio clip
    private AudioSource audioSource; // Reference to the AudioSource component
    private bool isPlaying = false; // Flag to check if the audio is currently playing

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.playOnAwake = false; // Do not play the sound immediately on awake
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the specified tag "lightbox"
        if (other.CompareTag("lightbox") && !isPlaying)
        {
            audioSource.Play();
            isPlaying = true; // Set flag to true to avoid replaying while still inside the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object has the specified tag "lightbox"
        if (other.CompareTag("lightbox") && isPlaying)
        {
            isPlaying = false; // Reset the flag when no longer illuminated by the lightbox
        }
    }
}