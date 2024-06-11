using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrRigStart : MonoBehaviour
{
    public Vector3 startPosition;
    public Quaternion startRotation;

    void Start()
    {
        // Set the initial position and rotation of the XR Rig
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}