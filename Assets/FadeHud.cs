using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeHud : MonoBehaviour
{
    public TextMeshPro textComponent; // Reference to the TextMeshPro component
    public GameObject secondaryObject; // Reference to another GameObject (e.g., a UI TextBox)

    public float fadeInDuration = 2f; // Duration for the fade-in effect
    public float fadeOutDuration = 2f; // Duration for the fade-out effect
    public float displayDuration = 5f; // Duration for which the HUD remains visible
    public float disappearDelay = 10f; // Delay before the text fades out

    public GameObject newHudObject; // Reference to the new HUD to activate after fading out

    private Coroutine fadeCoroutine; // Coroutine to manage the fade-in and fade-out process

    void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshPro>();
        }

        // Start the coroutine to handle the fade-in, display, and fade-out with a new HUD trigger
        fadeCoroutine = StartCoroutine(FadeInAndOutWithTrigger());
    }

    IEnumerator FadeInAndOutWithTrigger()
    {
        // Fade in the text and the secondary object
        if (textComponent != null)
        {
            yield return StartCoroutine(FadeTextIn(textComponent, fadeInDuration));
        }

        if (secondaryObject != null)
        {
            yield return StartCoroutine(FadeObjectIn(secondaryObject, fadeInDuration));
        }

        // Wait for the specified display duration
        yield return new WaitForSeconds(displayDuration);

        // Start the fade-out process and trigger a new HUD component after fading out
        yield return StartCoroutine(FadeOutAndTriggerNewHud());
    }

    IEnumerator FadeOutAndTriggerNewHud()
    {
        // Delay before starting the fade-out
        yield return new WaitForSeconds(disappearDelay);

        // Gradually fade out the text and the secondary object
        if (textComponent != null)
        {
            yield return StartCoroutine(FadeTextOut(textComponent, fadeOutDuration));
        }

        if (secondaryObject != null)
        {
            yield return StartCoroutine(FadeObjectOut(secondaryObject, fadeOutDuration));
        }

        // Activate the new HUD GameObject
        if (newHudObject != null)
        {
            newHudObject.SetActive(true);
        }
    }

    IEnumerator FadeTextIn(TextMeshPro text, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = text.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / duration); // Interpolate alpha from 0 to 1
            text.color = new Color(
                originalColor.r, originalColor.g, originalColor.b, alpha); // Update alpha
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        text.color = new Color(
            originalColor.r, originalColor.g, originalColor.b, 1); // Ensure alpha is 1 at the end
    }

    IEnumerator FadeTextOut(TextMeshPro text, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = text.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration); // Interpolate alpha from 1 to 0
            text.color = new Color(
                originalColor.r, originalColor.g, originalColor.b, alpha); // Update alpha
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        text.color = new Color(
            originalColor.r, originalColor.g, originalColor.b, 0); // Ensure alpha is 0 at the end
    }

    IEnumerator FadeObjectIn(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) yield break; // Return if there's no Renderer

        float elapsedTime = 0f;
        Color originalColor = renderer.material.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / duration); // Interpolate alpha
            renderer.material.color = new Color(
                originalColor.r, originalColor.g, originalColor.b, alpha); // Update alpha
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        renderer.material.color = new Color(
            originalColor.r, originalColor.g, originalColor.b, 1); // Ensure alpha is 1 at the end
    }

    IEnumerator FadeObjectOut(GameObject obj, float duration)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) yield break;

        float elapsedTime = 0f;
        Color originalColor = renderer.material.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / duration); // Interpolate alpha
            renderer.material.color = new Color(
                originalColor.r, originalColor.g, originalColor.b, alpha); // Update alpha
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        renderer.material.color = new Color(
            originalColor.r, originalColor.g, originalColor.b, 0); // Ensure alpha is 0 at the end
    }
}