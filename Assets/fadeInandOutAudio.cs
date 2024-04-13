using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInandOutAudio : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField]
    private float fadeInDuration = 2.0f;  // Default fade-in duration
    [SerializeField]
    private float fadeOutDuration = 2.0f; // Default fade-out duration

    void Awake()
    {
        // Ensure there is an AudioSource component attached
        if (!audioSource) audioSource = GetComponent<AudioSource>();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeAudio(0f, 1f, fadeInDuration, false)); // Linear fade in
    }

    public void FadeOut()
    {
        StartCoroutine(FadeAudio(1f, 0f, fadeOutDuration, true)); // Exponential fade out
    }

    private IEnumerator FadeAudio(float startLevel, float endLevel, float duration, bool isFadeOut)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume;

            if (isFadeOut)
            {
                // Exponential fade-out
                newVolume = Mathf.Lerp(startLevel, endLevel, 1 - Mathf.Exp(-4 * currentTime / duration));
            }
            else
            {
                // Linear fade-in
                newVolume = Mathf.Lerp(startLevel, endLevel, currentTime / duration);
            }

            audioSource.volume = newVolume;
            yield return null;
        }
        audioSource.volume = endLevel;

        // Automatically stop the audio when volume reaches 0 and it's a fade-out
        if (endLevel == 0f && isFadeOut)
        {
            audioSource.Stop();
        }
    }

    // Public methods to set durations dynamically
    public void SetFadeInDuration(float duration)
    {
        fadeInDuration = duration;
    }

    public void SetFadeOutDuration(float duration)
    {
        fadeOutDuration = duration;
    }
}