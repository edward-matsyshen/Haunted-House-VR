using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ClickTriggerAnim : MonoBehaviour
{
    [SerializeField] private GameObject[] HoverObjects;
    [SerializeField] private Animation leverAnimation; // Reference to the Animation component
    [SerializeField] private AnimationClip animationClip; // Reference to the AnimationClip
    [SerializeField] private float hoverDisplayDuration = 3f; // Duration to display hover objects after player entry

    private bool isPlayerClose = false; // To track if the player is close to the lever
    private bool isAnimationTriggered = false; // To prevent repeated triggering of the animation
    private bool hasHoverObjectsBeenDisplayed = false; // To track if the hover objects have been displayed
    private Coroutine hideHoverCoroutine; // Reference to the coroutine

    private void Update()
    {
        UpdateHoverObjectsState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isAnimationTriggered)
        {
            var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            bool leftHandSqueezePressed = false;
            bool rightHandSqueezePressed = false;

            leftHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out leftHandSqueezePressed);
            rightHandDevice.TryGetFeatureValue(CommonUsages.gripButton, out rightHandSqueezePressed);

            if (leftHandSqueezePressed || rightHandSqueezePressed)
            {
                PlayLeverAnimation();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasHoverObjectsBeenDisplayed) // Ensure your player GameObject has a "Player" tag and hover objects haven't been displayed
        {
            isPlayerClose = true;
            hasHoverObjectsBeenDisplayed = true; // Mark hover objects as displayed
            if (hideHoverCoroutine != null)
            {
                StopCoroutine(hideHoverCoroutine);
            }
            hideHoverCoroutine = StartCoroutine(HideHoverObjectsAfterDelay());
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

    private IEnumerator HideHoverObjectsAfterDelay()
    {
        yield return new WaitForSeconds(hoverDisplayDuration);

        foreach (GameObject hoverObject in HoverObjects)
        {
            hoverObject.SetActive(false);
        }
    }
}