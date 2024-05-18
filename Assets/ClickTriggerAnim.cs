using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ClickTriggerAnim : MonoBehaviour
{
    [SerializeField] private GameObject[] HoverObjects;
    [SerializeField] private Animation leverAnimation; // Reference to the Animation component
    [SerializeField] private AnimationClip animationClip; // Reference to the AnimationClip

    private bool isPlayerClose = false; // To track if the player is close to the lever
    private bool isAnimationTriggered = false; // To prevent repeated triggering of the animation

    private void Update()
    {
        UpdateHoverObjectsState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isAnimationTriggered)
        {
            var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

            if (triggerPressed)
            {
                PlayLeverAnimation();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player GameObject has a "Player" tag
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }

    private void PlayLeverAnimation()
    {
        leverAnimation.clip = animationClip; // Set the animation clip
        leverAnimation.Play(); // Play the animation
        isAnimationTriggered = true; // Mark the animation as triggered to prevent re-triggering
    }

    private void UpdateHoverObjectsState()
    {
        foreach (GameObject hoverObject in HoverObjects)
        {
            hoverObject.SetActive(isPlayerClose);
        }
    }
}