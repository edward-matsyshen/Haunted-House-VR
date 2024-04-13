using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modelRotateWhenNotLooking : MonoBehaviour
{
    public Transform modelRotate;
    private Renderer objectRenderer;
    private Camera mainCamera;

    void Start()
    {
        // Get the Renderer component of the object
        objectRenderer = GetComponent<Renderer>();
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the object is visible to the main camera
        if (!IsVisibleFrom(mainCamera, objectRenderer))
        {
            // Rotate model to direction of main camera.
            transform.LookAt(modelRotate);  
        }
    }

    bool IsVisibleFrom(Camera camera, Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

}
