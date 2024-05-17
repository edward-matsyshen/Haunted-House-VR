using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLookWithinDistance : MonoBehaviour
{
    public Transform eyeDest;
    public float lookDistance = 4.0f;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, eyeDest.position);

        // Check if the distance is less than or equal to the threshold
        if (distance <= lookDistance)
        {
            // Make the eyes look at the target
            transform.LookAt(eyeDest);
        }
    }
}
