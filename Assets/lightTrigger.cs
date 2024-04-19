using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTrigger : MonoBehaviour
{
    public Light directionalLight;  // Reference to the directional light
    public float flickerDuration = 5.0f;  // Total duration of the flickering in seconds
    public int flickerCount = 10;  // Number of times the light flickers on and off

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (directionalLight != null)
            {
                StartCoroutine(FlickerLight());
            }
            else
            {
                Debug.LogWarning("No directional light found in the scene.");
            }
        }
    }

    private IEnumerator FlickerLight()
    {
        // Turn off the light initially
        directionalLight.enabled = false;
        Debug.Log("Directional light turned off initially.");
        yield return new WaitForSeconds(5.0f);  // Wait for 5 seconds

        // Flickering effect
        for (int i = 0; i < flickerCount; i++)
        {
            directionalLight.enabled = !directionalLight.enabled;  // Toggle light on and off
            yield return new WaitForSeconds(flickerDuration / flickerCount);  // Wait between toggles
        }

        // Ensure the light is turned off after flickering
        directionalLight.enabled = false;
        Debug.Log("Directional light turned off permanently after flickering.");
    }
}