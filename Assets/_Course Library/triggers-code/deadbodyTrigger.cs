using UnityEngine.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections; // Required for coroutines

public class deadbodyTrigger : MonoBehaviour
{
    public Animation bodyAnimation;
    public AudioSource deadBodyFloatUpAudio;
    public Transform playerTransform; // Assign the player's transform in the inspector
    public float triggerDistance = 1.0f; // The distance within which the animation should trigger

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
        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Play the animation
        bodyAnimation.Play();

        // Check if the audio source is available and an audio clip is assigned
        if (deadBodyFloatUpAudio != null && deadBodyFloatUpAudio.clip != null)
        {
            deadBodyFloatUpAudio.Play();
        }
        else
        {
            Debug.LogWarning("Dead body audio source is missing or no audio clip is assigned.", this);
        }

        // Optionally, disable the game object after triggering
        gameObject.SetActive(false);
    }
}
