using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTrigger : MonoBehaviour
{
    public Light directionalLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (directionalLight != null)
            {
                // Turn off the directional light
                directionalLight.enabled = false;
                Debug.Log("Directional light turned off.");
            }
            else
            {
                Debug.LogWarning("No directional light found in the scene.");
            }
        }
    }
}