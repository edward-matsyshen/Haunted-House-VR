using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequintalSoundwithFadeOut : MonoBehaviour
{
    public AudioClip[] audioClips;  // Array of audio clips to play sequentially
    public float volume = 1.0f;  // Volume level of the audio clips (0.0f to 1.0f)
    public float fadeInDuration = 2.0f;  // Duration in seconds for the audio to fade in
    public float fadeOutDuration = 2.0f;  // Duration in seconds for the audio to fade out
    public float delayBetweenClips = 3.0f;  // Delay in seconds between audio clips

    private AudioSource audioSource;  // Reference to the AudioSource component
    private Collider collider;  // Reference to the Collider component
    private bool audioPlayed = false;  // Flag to check if the audio has already been played

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = 0;  // Start with volume at 0 for fade in
        audioSource.playOnAwake = false;

        collider = GetComponent<Collider>();
        if (collider == null || !collider.isTrigger)
        {
            Debug.LogError("Collider component not found or not set as trigger.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !audioPlayed)
        {
            StartCoroutine(FadeInAudio());
            audioPlayed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOutAudio());
        }
    }

    private IEnumerator PlayAudioClips()
    {
        foreach (var clip in audioClips)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length + delayBetweenClips);
        }
    }

    private IEnumerator FadeInAudio()
    {
        float startVolume = 0;
        audioSource.volume = startVolume;
        while (audioSource.volume < volume)
        {
            audioSource.volume += volume * Time.deltaTime / fadeInDuration;
            yield return null;
        }
        StartCoroutine(PlayAudioClips());
    }

    private IEnumerator FadeOutAudio()
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = volume;  // Reset the volume
        audioPlayed = false;  // Allow the audio to be triggered again
    }
}