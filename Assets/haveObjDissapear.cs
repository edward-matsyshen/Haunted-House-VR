using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haveObjDissapear : MonoBehaviour
{
    public float hideDistance = 10.0f;
    private Camera mainCamera;

    // References to objects that should appear/disappear
    public GameObject gameObject1;
    public GameObject gameObject2;

    private Quaternion lastCameraRotation; // To keep track of camera's last rotation
    private bool isObject1LastViewed = false;
    private bool isObject2LastViewed = false;

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraRotation = mainCamera.transform.rotation;

        // Initially hide both game objects
        if (gameObject1 != null)
            gameObject1.SetActive(false);
        if (gameObject2 != null)
            gameObject2.SetActive(false);
    }

    void Update()
    {
        if (mainCamera != null && Vector3.Distance(transform.position, mainCamera.transform.position) < hideDistance)
        {
            CheckCameraRotationChange();
        }
    }

    void CheckCameraRotationChange()
    {
        // Check the difference in rotation
        Quaternion currentRotation = mainCamera.transform.rotation;
        float yRotationDifference = currentRotation.eulerAngles.y - lastCameraRotation.eulerAngles.y;

        if (Mathf.Abs(yRotationDifference) > 1) // Check if there has been a significant rotation
        {
            if (yRotationDifference > 0)
            {
                // Turning right
                if (gameObject1 != null && gameObject1.activeSelf)
                {
                    // If gameObject1 was visible when turning began
                    isObject1LastViewed = true;
                }

                if (gameObject2 != null)
                    gameObject2.SetActive(true);
            }
            else if (yRotationDifference < 0)
            {
                // Turning left
                if (gameObject2 != null && gameObject2.activeSelf)
                {
                    // If gameObject2 was visible when turning began
                    isObject2LastViewed = true;
                }

                if (gameObject1 != null)
                    gameObject1.SetActive(true);
            }

            // Destroy objects if the direction changes and an object was previously viewed
            if (isObject1LastViewed || isObject2LastViewed)
            {
                DestroyObjects();
            }

            // Update lastCameraRotation
            lastCameraRotation = currentRotation;
        }
    }

    void DestroyObjects()
    {
        if (gameObject1 != null)
            Destroy(gameObject1);

        if (gameObject2 != null)
            Destroy(gameObject2);

        // Reset view flags
        isObject1LastViewed = false;
        isObject2LastViewed = false;
    }
}