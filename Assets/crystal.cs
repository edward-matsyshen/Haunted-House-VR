using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))] // Ensure there's a collider on the GameObject
public class RotateOnTrigger : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float brightenSpeed = 0.01f;
    private bool shouldRotate = false;
    private bool doneRotating = false;
    private Renderer objectRenderer;
    private Color emissiveColor;
    private Color objectColor;
    private Material objectMaterial;
    private Collider collider;
    public AudioClip audioClip;
    public float volume = 0.5f;
    private AudioSource audioSource;
    public GameObject[] objectsToReveal;

    // Add a static counter to track the number of crystals rotated
    public static int crystalsRotated = 0;
    public int totalCrystals = 3;  // Set the total number of crystals
    public GameObject objectToDisable; // GameObject to disable when all crystals are rotated

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;
        objectColor = objectRenderer.material.color;
        if (objectMaterial.HasProperty("_EmissionColor"))
        {
            emissiveColor = objectMaterial.GetColor("_EmissionColor");
        }
        else
        {
            emissiveColor = Color.black;
        }

        objectMaterial.EnableKeyword("_EMISSION");
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        foreach (GameObject obj in objectsToReveal)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("lightbox"))
        {
            shouldRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("lightbox"))
        {
            shouldRotate = false;
        }
    }

    private void Update()
    {
        if (shouldRotate && !doneRotating)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            UpdateColors();
        }
    }

    private void UpdateColors()
    {
        if (objectColor.r <= 0.9)
        {
            objectColor += new Color(brightenSpeed, brightenSpeed, brightenSpeed) * Time.deltaTime;
            emissiveColor += new Color(brightenSpeed, brightenSpeed, brightenSpeed) * Time.deltaTime;

            objectRenderer.material.color = objectColor;
            objectMaterial.SetColor("_EmissionColor", emissiveColor);
        }
        else if (!doneRotating)
        {
            FinishRotation();
        }
    }

    private void FinishRotation()
    {
        objectColor = Color.white;
        emissiveColor = Color.white;

        objectRenderer.material.color = objectColor;
        objectMaterial.SetColor("_EmissionColor", emissiveColor);

        if (audioClip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.volume = volume;
        }

        foreach (GameObject obj in objectsToReveal)
        {
            obj.SetActive(true);
        }

        doneRotating = true;
        crystalsRotated++;
        CheckAllCrystalsRotated();
    }

    private void CheckAllCrystalsRotated()
    {
        if (crystalsRotated >= totalCrystals)
        {
            objectToDisable.SetActive(false);
        }
    }
}