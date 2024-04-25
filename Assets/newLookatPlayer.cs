using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newLookatPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform

    void Update()
    {
        if (player != null)
        {
            // Calculate direction from hover object to player
            Vector3 direction = player.position - transform.position;

            // Determine the new rotation that points to the player
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Apply the rotation to the hover GameObject
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}