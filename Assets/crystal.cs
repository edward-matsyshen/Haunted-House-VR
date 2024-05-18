using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EmissiveObjectController : MonoBehaviour
{
    public GameObject emissiveObject; // The object that will become brighter
    public GameObject[] objectsToReveal; // Objects to be made visible
    public float emissiveIntensity = 5.0f; // Intensity of the emissive property
    public float revealDelay = 3.0f; // Delay before revealing objects

    private Material emissiveMaterial; // Material of the emissive object
    private Color originalEmissionColor; // Original emission color of the material

    void Start()
    {
        if (emissiveObject != null)
        {
            Renderer renderer = emissiveObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                emissiveMaterial = renderer.material;
                originalEmissionColor = emissiveMaterial.GetColor("_EmissionColor");
            }
        }

        // Initially hide all objects to be revealed
        foreach (GameObject obj in objectsToReveal)
        {
            obj.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (emissiveMaterial != null)
        {
            // Make the emissive object brighter
            emissiveMaterial.SetColor("_EmissionColor", originalEmissionColor * emissiveIntensity);
            StartCoroutine(RevealObjectsAfterDelay(revealDelay));
        }
    }

    IEnumerator RevealObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Make all specified objects visible
        foreach (GameObject obj in objectsToReveal)
        {
            obj.SetActive(true);
        }
    }
}
