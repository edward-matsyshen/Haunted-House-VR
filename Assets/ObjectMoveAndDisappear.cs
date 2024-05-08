using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveAndDisappear : MonoBehaviour
{
    public Vector3 translationVector = new Vector3(0f, 0f, 1f); // Direction and speed of movement
    public float distanceToDisappear = 2f; // Distance after which the object will disappear
    private float traveledDistance = 0f;
    private bool startMoving = false; // Flag to control when the object starts moving

    void Update()
    {
        // Check if the object should start moving
        if (startMoving)
        {
            // Calculate movement for this frame based on frame time and translation vector
            Vector3 movement = translationVector * Time.deltaTime;
            transform.Translate(movement);

            // Update the total traveled distance
            traveledDistance += movement.magnitude;

            // Check if the object has moved the required distance
            if (traveledDistance >= distanceToDisappear)
            {
                // Deactivate the object (you can also use Destroy(gameObject) if you want to completely remove it)
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("MainCamera"))
        {
            startMoving = true; // Set the flag to start moving the object
        }
    }
}
    