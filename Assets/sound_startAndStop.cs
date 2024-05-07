using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] // Ensure there's a collider on the GameObject
public class AudioController : MonoBehaviour
{
    public AudioClip audioClip; // The audio clip to play
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Add an AudioSource component to this GameObject if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the AudioClip to the AudioSource
        audioSource.clip = audioClip;

        // Ensure the audio will loop
        audioSource.loop = true;

        // Initially, do not play the audio
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.CompareTag("Player"))
        {
            // Start playing the audio when the player enters the collision box
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is the player
        if (other.CompareTag("Player"))
        {
            // Stop playing the audio when the player leaves the collision box
            audioSource.Stop();
        }
    }
}
