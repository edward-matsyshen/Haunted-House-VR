using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveAndDisappear : MonoBehaviour
{
    public Vector3 translationVector = new Vector3(1f, 0f, 0f); 
    public float distanceToDisappear = 5f;
    public float triggerDistance = 3f; 

    private float traveledDistance = 0f;
    private bool startMoving = false; 
    private Renderer objectRenderer;
    private Camera mainCamera;

    void Start()
    {
        
        objectRenderer = GetComponent<Renderer>();

        
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the main camera is within the distance
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
                
                gameObject.SetActive(false); 
            }
        }
        else
        {
            
            gameObject.SetActive(true); 
        }
    }
}
    