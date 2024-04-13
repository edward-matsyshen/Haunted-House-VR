using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideWhenNotLooking : MonoBehaviour
{
    public float hideDistance = 10.0f;
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
        // Check if the main camera is within the hide distance
        if (mainCamera != null && Vector3.Distance(transform.position, mainCamera.transform.position) < hideDistance)
        {
<<<<<<< Updated upstream
            // Check if the main camera and the object renderer are valid
            if (objectRenderer != null)
            {
                // Check if the object is visible to the main camera
                if (!IsVisibleFrom(mainCamera, objectRenderer))
                {
                    // If the object is not visible, hide or deactivate it
                    gameObject.SetActive(false); // Alternatively, you can use objectRenderer.enabled = false;
                }
                else
                {
                    // If the object is visible, ensure it's active or visible
                    gameObject.SetActive(true); // Alternatively, you can use objectRenderer.enabled = true;
                }
            }
            else
            {
                // Log a warning if the object renderer is not found
                Debug.LogWarning("Object renderer not found!");
=======
            // Check if the object is visible to the main camera
            if (!IsVisibleFrom(mainCamera, objectRenderer))
            {
                // If the object is not visible, hide or deactivate it
                gameObject.SetActive(false); // Alternatively, you can use objectRenderer.enabled = false;
            }
            else
            {
                // If the object is visible, ensure it's active or visible
                gameObject.SetActive(true); // Alternatively, you can use objectRenderer.enabled = true;
>>>>>>> Stashed changes
            }
        }
        else
        {
            // If the main camera is outside of the hide distance, ensure the object is active
            gameObject.SetActive(true);
        }
    }

    // Function to check if the object is visible to the camera
    bool IsVisibleFrom(Camera camera, Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
