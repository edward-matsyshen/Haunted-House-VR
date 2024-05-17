using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAnim : MonoBehaviour
{
    public Animation animation;
    public AudioSource animationFloatUpAudio;
    public AudioSource additionalAudio; // Optional additional audio track
    public Transform playerTransform; // Assign the primary player's transform in the inspector
    public Transform optionalPlayerTransform; // Optional secondary player transform
    public GameObject objectToEnable; // Game object to enable when trigger is entered
    public float triggerDistance = 1.0f; // Distance within which the animation should trigger
    public float delayBeforeTrigger = 2.0f; // Delay before triggering animation and audio

    private void Update()
    {
        // Calculate the distance between the primary player and this object
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // If a secondary player transform is assigned, check the distance to it as well
        float distanceToOptionalPlayer = optionalPlayerTransform != null ? Vector3.Distance(optionalPlayerTransform.position, transform.position) : float.MaxValue;

        // Check if either player is close enough and the animation is not already playing
        if ((distanceToPlayer <= triggerDistance || distanceToOptionalPlayer <= triggerDistance) && !animation.isPlaying && !objectToEnable.activeSelf)
        {
            StartCoroutine(DelayedTriggerAnimationAndSound());
        }
    }

    private IEnumerator DelayedTriggerAnimationAndSound()
    {
        // Wait for the specified delay before triggering
        yield return new WaitForSeconds(delayBeforeTrigger);

        // Enable the game object
        objectToEnable.SetActive(true);

        // Play the animation
        animation.Play();

        // Play the primary audio if available
        if (animationFloatUpAudio != null && animationFloatUpAudio.clip != null)
        {
            animationFloatUpAudio.Play();
        }
        else
        {
            Debug.LogWarning("Animation audio source is missing or no audio clip is assigned.", this);
        }

        // Optionally, play the additional audio if available
        if (additionalAudio != null && additionalAudio.clip != null)
        {
            additionalAudio.Play();
        }
        else if (additionalAudio != null)
        {
            Debug.LogWarning("Additional audio source is missing or no audio clip is assigned.", this);
        }
    }
}