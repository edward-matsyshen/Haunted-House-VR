using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class skeleDestroy : MonoBehaviour
{
    public Animation skeleAnimation;
    public AudioSource skeleAudio;
    public Transform playerTransform; // Assign the player's transform in the inspector
    public float triggerDistance = 1.0f; // The distance within which the animation should trigger

    private void Update()
    {
        // Calculate the distance between the player and this object
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // Check if the player is close enough and the animation is not already playing
        if (distanceToPlayer <= triggerDistance && !skeleAnimation.isPlaying)
        {
            TriggerAnimationAndSound();
        }
    }

    private void TriggerAnimationAndSound()
    {
        // Play the animation
        skeleAnimation.Play();

        // Check if the audio source is available and an audio clip is assigned
        if (skeleAudio != null && skeleAudio.clip != null)
        {
            skeleAudio.Play();
        }
        else
        {
            Debug.LogWarning("Skele audio source is missing or no audio clip is assigned.", this);
        }

        // Optionally, disable the game object after triggering
        gameObject.SetActive(false);
    }
}