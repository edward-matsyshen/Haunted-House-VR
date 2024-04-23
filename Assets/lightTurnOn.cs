using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTurnOn : MonoBehaviour
{
    public Light directionalLight;  // Reference to the directional light

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            directionalLight.enabled = true;
            Debug.Log("Directional light on");
        }
    }
}
