using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearScript : MonoBehaviour
{
    public GameObject ScareObject; // Reference to the banana man GameObject
    public float requiredStayTime = 3f; // Duration required for the player to stay within the collider
    public float disappearTime = 5f; // Time after which the object will disappear once activated

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

            // If the required duration is met, deactivate the banana man
            if (stayTimer >= requiredStayTime && ScareObject.activeSelf)
            {
                Invoke("DeactivateScareObject", disappearTime); // Schedule deactivation after specified time
            }
        }
    }

    private void DeactivateScareObject()
    {
        ScareObject.SetActive(false);
    }
}