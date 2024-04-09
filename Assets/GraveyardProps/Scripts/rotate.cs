using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    private float y = 0.0f;
    public float rotationSpeed = 1f; // You can adjust this value in the inspector for a faster or slower rotation

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Increment y by a small value multiplied by deltaTime
        // This will make the rotation speed consistent across different frame rates
        // Decrease rotationSpeed or adjust the multiplier for a slower rotation
        y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, y, 0);
    }
}
