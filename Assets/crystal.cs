using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] // Ensure there's a collider on the GameObject
public class RotateOnTrigger : MonoBehaviour
{    
    // Rotation speed
    public float rotationSpeed = 90f;
    // Speed at which the emissive color will brighten
    public float brightenSpeed = 0.01f;
    // Flag to check if the object is inside the trigger
    private bool shouldRotate = false;
    private bool doneRotating = false;
    private Renderer objectRenderer;
    private Color emissiveColor;
    private Color objectColor;
    private Material objectMaterial;

    private Collider collider;  // Reference to the Collider component

    public AudioClip audioClip;  // The audio clip to play
    public float volume = 1.0f;  // Volume level of the audio clip (0.0f to 1.0f)
    private AudioSource audioSource;  // Reference to the AudioSource component

    public GameObject[] objectsToReveal; // Objects to be made visible

    void Start()
    {
        // Get the Renderer component from the GameObject
        objectRenderer = GetComponent<Renderer>();
        
        // Get the material of the Renderer
        objectMaterial = objectRenderer.material;
        // Get the initial color of the object
        objectColor = objectRenderer.material.color;
        // Get the initial emissive color of the material
        if (objectMaterial.HasProperty("_EmissionColor"))
        {
            emissiveColor = objectMaterial.GetColor("_EmissionColor");
        }
        else
        {
            emissiveColor = Color.black;
        }

        // Enable emission keyword if not already enabled
        objectMaterial.EnableKeyword("_EMISSION");

        // Get the Collider component attached to the same GameObject
        collider = GetComponent<Collider>();

        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource component attached, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the audio clip to the AudioSource component
        audioSource.clip = audioClip;
        audioSource.volume = volume;  // Set the initial volume

        // Initially hide all objects to be revealed
        foreach (GameObject obj in objectsToReveal)
        {
            obj.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the specified tag
        if (other.CompareTag("lightbox"))
        {
            // Start rotating
            shouldRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object has the specified tag
        if (other.CompareTag("lightbox"))
        {
            // Stop rotating
            shouldRotate = false;
        }
    }

    private void Update()
    {
        // Rotate the object if the flag is set
        if (shouldRotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            if(objectColor.r <= 0.9 && !doneRotating){

                
                // Increase the brightness of the color
                objectColor.r = Mathf.Clamp01(objectColor.r + brightenSpeed * Time.deltaTime);
                objectColor.g = Mathf.Clamp01(objectColor.g + brightenSpeed * Time.deltaTime);
                objectColor.b = Mathf.Clamp01(objectColor.b + brightenSpeed * Time.deltaTime);
            
                // Increase the brightness of the emissive color
                emissiveColor.r = Mathf.Clamp01(emissiveColor.r + brightenSpeed * Time.deltaTime);
                emissiveColor.g = Mathf.Clamp01(emissiveColor.g + brightenSpeed * Time.deltaTime);
                emissiveColor.b = Mathf.Clamp01(emissiveColor.b + brightenSpeed * Time.deltaTime);
        
                // Apply the new color to the material
                objectRenderer.material.color = objectColor;

                // Apply the new emissive color to the material
                objectMaterial.SetColor("_EmissionColor", emissiveColor);
            }
            if(objectColor.r > 0.9 && !doneRotating){
                objectColor.r = 1.0f;
                objectColor.g = 1.0f;
                objectColor.b = 1.0f;

                emissiveColor.r = 1.0f;
                emissiveColor.g = 1.0f;
                emissiveColor.b = 1.0f;

                objectRenderer.material.color = objectColor;
                objectMaterial.SetColor("_EmissionColor", emissiveColor);

                // Check if the audio clip is assigned and not currently playing
                if (audioClip != null && !audioSource.isPlaying)
                {
                    // Play the audio clip
                    audioSource.Play();

                    // Set the volume of the audio clip
                    audioSource.volume = volume;

                    // Disable the collider to prevent further triggers
                    collider.enabled = false;
                }

                // Make all specified objects visible
                foreach (GameObject obj in objectsToReveal)
                {
                    obj.SetActive(true);
                }

                doneRotating = true;
            }
        }
    }
}
