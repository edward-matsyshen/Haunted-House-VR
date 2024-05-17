using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVaudio01 : MonoBehaviour
{
    public AudioClip audioClip;  // The audio clip to play
    public float volume = 1.0f;  // Maximum volume level of the audio clip (0.0f to 1.0f)
    public float maxDistance = 10.0f;  // Max distance at which audio is heard
    public float minDistance = 1.0f;   // Distance below which volume is max and does not decrease

    private AudioSource audioSource;  // Reference to the AudioSource component
    private Collider collider;  // Reference to the Collider component
    private AudioReverbFilter reverbFilter;  // Reference to the AudioReverbFilter component

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 1.0f;  // Set to 3D sound
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.rolloffMode = AudioRolloffMode.Linear;  // Set volume rolloff

        reverbFilter = gameObject.AddComponent<AudioReverbFilter>();
        reverbFilter.reverbPreset = AudioReverbPreset.Hallway;  // Example preset

        collider = GetComponent<Collider>();
        if (collider == null || !collider.isTrigger)
        {
            Debug.LogError("Collider component not found or not set as trigger.");
        }
        else
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioClip != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                UpdateAudioEffects(other.transform.position);
                collider.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            UpdateAudioEffects(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }

    private void UpdateAudioEffects(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        audioSource.volume = Mathf.Clamp(volume * (1 - (distance / maxDistance)), 0, volume);

        // Adjust reverb level based on distance (optional, adjust to taste)
        reverbFilter.room = (int)Mathf.Lerp(-10000, 0, 1 - (distance / maxDistance));
    }
}