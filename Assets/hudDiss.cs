using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hudDiss : MonoBehaviour
{
    public TextMeshPro textComponent; // Reference to the TextMeshPro component
    public float blinkInterval = 0.5f; // Interval between blinks
    public int numberOfBlinks = 5; // Total number of blinks before disappearing
    public float disappearDelay = 10.0f; // Time before the text disappears completely

    private Coroutine disappearCoroutine; // Coroutine to handle blinking and disappearing

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshPro>();
        }

        // Start the coroutine to handle blinking and disappearance
        disappearCoroutine = StartCoroutine(BlinkAndDisappear());
    }

    IEnumerator BlinkAndDisappear()
    {
        // Wait for the time minus the blinking period
        yield return new WaitForSeconds(disappearDelay - (numberOfBlinks * blinkInterval));

        // Blink the text several times
        for (int i = 0; i < numberOfBlinks; i++)
        {
            textComponent.enabled = !textComponent.enabled; // Toggle text visibility
            yield return new WaitForSeconds(blinkInterval); // Wait between blinks
        }

        textComponent.enabled = false; // Hide the text after blinking
    }

    void OnDisable()
    {
        // Stop the coroutine when the script is disabled
        if (disappearCoroutine != null)
        {
            StopCoroutine(disappearCoroutine);
        }
    }
}