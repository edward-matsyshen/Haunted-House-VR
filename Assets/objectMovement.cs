using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovement : MonoBehaviour
{
    public float destinationX = 25.0f; // Desired x-coordinate of the destination
    public float detectionRange = 5.0f; // Range within which the main camera triggers movement
    public float movementSpeed = 20.0f; // Speed of movement

    private Vector3 targetPosition;
    private bool isMoving = false;
    private Camera mainCamera;

    void Start()
    {
        // Calculate the target position with the desired x-coordinate
        targetPosition = new Vector3(destinationX, transform.position.y, transform.position.z);

        // Find the main camera in the scene
        mainCamera = Camera.main;

        // Debug.Log if main camera is not found
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found!");
        }
    }

    void Update()
    {
        // Check if the main camera is within the detection range
        if (!isMoving && mainCamera != null && Vector3.Distance(transform.position, mainCamera.transform.position) < detectionRange)
        {
            // Start moving the object towards the target position
            isMoving = true;
        }

        // If the object should move, move it towards the target position
        if (isMoving)
        {
            // Move the object towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            // Debug.Log when the object starts moving
            Debug.Log("Object is moving...");

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // Log when the object reaches the destination
                Debug.Log("Object reached destination.");
                isMoving = false; // Stop moving once reached the destination
            }
        }
    }

}
