using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjectMovementAudio : MonoBehaviour
{
    public AudioClip movingSoundClip;      // Audio clip to play when the object is moving
    public AudioClip stationarySoundClip;  // Audio clip to play when the object is stationary
    public float volume = 1.0f;            // Volume level for the audio clips
    public float minVelocity = 0.0f;            // Minimum speed to generate noise

    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;  // Set the initial volume
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if the current position is different from the last recorded position
        if (Mathf.Sqrt((transform.position.x - lastPosition.x)*(transform.position.x - lastPosition.x) + (transform.position.y - lastPosition.y)*(transform.position.y - lastPosition.y) + (transform.position.z - lastPosition.z)*(transform.position.z - lastPosition.z)) > minVelocity)
        {
            // Object is moving
            if (!audioSource.isPlaying || audioSource.clip != movingSoundClip) // Check if moving sound is not already playing
            {
                audioSource.clip = movingSoundClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            // Object is not moving
            if (!audioSource.isPlaying || audioSource.clip != stationarySoundClip) // Check if stationary sound is not already playing
            {
                audioSource.clip = stationarySoundClip;
                audioSource.loop = false; // Play once when stationary
                audioSource.Play();
            }
        }

        // Update volume in case it was changed externally
        audioSource.volume = volume;

        // Update lastPosition to the current position
        lastPosition = transform.position;
    }
}



