using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDissapear : MonoBehaviour
{
    public LayerMask lightLayer; // Layer to identify the light source
    private MeshRenderer meshRenderer; // MeshRenderer component to control visibility

    void Start()
    {
        // Initialize the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        RaycastHit hit;
        // Cast a ray from the light source towards this object
        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, lightLayer))
        {
            // Check if the hit object has the tag "lightbox"
            if (hit.collider.CompareTag("lightbox"))
            {
                // Hide the mesh if the lightbox is shining on it
                meshRenderer.enabled = false;
            }
        }
        else
        {
            // Show the mesh if the lightbox is not shining on it
            meshRenderer.enabled = true;
        }
    }
}