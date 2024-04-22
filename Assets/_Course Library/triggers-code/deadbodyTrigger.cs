using UnityEngine.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections; // Required for coroutines

public class deadbodyTrigger : MonoBehaviour
{
    public Animation bodyAnimation;
    public AudioSource deadBodyFloatUpAudio;
    public AudioSource additionalAudio; // New field for the optional additional audio track
    public Transform playerTransform; // Assign the player's transform in the inspector
    public float triggerDistance = 1.0f; // The distance within which the animation should trigger
    public float delayBeforeTrigger = 2.0f; // Delay before triggering animation and audio

    private void Update()
    {
        // Calculate the distance between the player and this object
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        // Check if the player is close enough and the animation is not already playing
        if (distanceToPlayer <= triggerDistance && !bodyAnimation.isPlaying)
        {
            StartCoroutine(DelayedTriggerAnimationAndSound());
        }
    }

    private IEnumerator DelayedTriggerAnimationAndSound()
    {
        // Wait for a specific delay before triggering
        yield return new WaitForSeconds(delayBeforeTrigger);

        // Play the animation
        bodyAnimation.Play();

        // Play the primary audio if available
        if (deadBodyFloatUpAudio != null && deadBodyFloatUpAudio.clip != null)
        {
            deadBodyFloatUpAudio.Play();
        }
        else
        {
            Debug.LogWarning("Dead body audio source is missing or no audio clip is assigned.", this);
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

        // Optionally, disable the game object after triggering
        gameObject.SetActive(false);
    }
}