using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject bananaMan; // Reference to the banana man GameObject
    public float requiredStayTime = 3f; // Duration required for the player to stay within the collider
    private float stayTimer = 0f; // Timer to track how long the player stays within the collider

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the collider of Object A
        if (other.CompareTag("Player"))
        {
            // Reset the stay timer when the player enters the collider
            stayTimer = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the player stays within the collider of Object A
        if (other.CompareTag("Player"))
        {
            // Increment the stay timer while the player is inside the collider
            stayTimer += Time.deltaTime;

            // If the required duration is met, activate the banana man
            if (stayTimer >= requiredStayTime)
            {
                bananaMan.SetActive(true);
            }
        }
    }
}
