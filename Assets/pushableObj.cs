using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushableObj : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f; // Force applied when pushing the object

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the XR Rig
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate the direction to apply the force
            Vector3 pushDirection = collision.contacts[0].point - transform.position;
            pushDirection = -pushDirection.normalized; // Normalize and invert the direction

            // Apply the force to the Rigidbody
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}