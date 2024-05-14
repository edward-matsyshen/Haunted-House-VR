using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideWhenNotLooking : MonoBehaviour
{
    public Vector3 translationVector = new Vector3(1f, 0f, 0f); // Direction and speed of movement
    public float distanceToDisappear = 5f; // Distance after which the object will disappear
    public float triggerDistance = 3f; // Distance at which the object starts moving

    private float traveledDistance = 0f;
    private bool startMoving = false; // Flag to control when the object starts moving
    private Renderer objectRenderer;
    private Camera mainCamera;

    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the main camera is within the trigger distance
        if (mainCamera != null && Vector3.Distance(transform.position, mainCamera.transform.position) < triggerDistance)
        {
            startMoving = true;
        }

        // If flagged to start moving
        if (startMoving)
        {
            Vector3 movement = translationVector * Time.deltaTime;
            transform.Translate(movement);
            traveledDistance += movement.magnitude;

            if (traveledDistance >= distanceToDisappear)
            {
                // If the object has moved the required distance, hide or deactivate it
                gameObject.SetActive(false); // Alternatively, you can use objectRenderer.enabled = false;
            }
        }
        else
        {
            // If the main camera is outside of the trigger distance, ensure the object is active
            gameObject.SetActive(true); // Alternatively, you can use objectRenderer.enabled = true;
        }
    }
}
