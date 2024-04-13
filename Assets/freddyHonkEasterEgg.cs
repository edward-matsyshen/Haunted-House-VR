using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freddyHonkEasterEgg : MonoBehaviour
{
    public AudioClip soundcue;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Make sure the object has an AudioSource component attached
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnMouseDown()
    {
        // Play the sound cue when the object is clicked
        if (soundcue != null)
        {
            audioSource.PlayOneShot(soundcue);
        }
    }
}
