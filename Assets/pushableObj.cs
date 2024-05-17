using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableObj : MonoBehaviour
{
    public GameObject objectToPush; // Assign this in the inspector
    public Vector3 pushDirection = Vector3.forward; // Default push direction
    public float pushForce = 5f; // Push force magnitude

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player")) // Make sure the player has a tag "Player"
        {
            Rigidbody rb = objectToPush.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply a force to the object's Rigidbody
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}