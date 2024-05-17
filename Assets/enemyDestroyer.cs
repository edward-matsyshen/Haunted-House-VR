using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mutant"))  // Ensure the mutant has a tag "Mutant"
        {
            Destroy(other.gameObject);  // Destroy the mutant
        }
    }
}