using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public GameObject objectWithAnimation; // Reference to the GameObject that contains the Animator component
    private Animator objectAnimator; // Reference to the Animator component of the object with the animation

    private void Start()
    {
        // Ensure that the objectWithAnimation GameObject is assigned
        if (objectWithAnimation != null)
        {
            // Get the Animator component from the objectWithAnimation GameObject
            objectAnimator = objectWithAnimation.GetComponent<Animator>();

            if (objectAnimator == null)
            {
                Debug.LogWarning("Animator component not found on the specified object.");
            }
        }
        else
        {
            Debug.LogWarning("Please assign the GameObject with the Animator component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as "Player"
        if (other.CompareTag("Player") && objectAnimator != null)
        {
            // Trigger the animation on the objectWithAnimation GameObject
            objectAnimator.SetTrigger("Open"); // Replace "TriggerAnimation" with your animation trigger name
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as "Player"
        if (other.CompareTag("Player") && objectAnimator != null)
        {
            // Reverse the animation on the objectWithAnimation GameObject
            objectAnimator.SetTrigger("Close"); // Replace "ReverseAnimation" with your animation reverse trigger name
        }
    }
}

