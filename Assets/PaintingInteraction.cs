using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PaintingInteraction : MonoBehaviour
{
    public static int correctAlignments = 0; // Static counter for aligned paintings
    public Transform bookshelf; // Reference to the bookshelf's transform
    public Vector3 targetPosition; // Target position for the bookshelf to move to
    public float moveSpeed = 2f; // Speed at which the bookshelf moves
    public Collider interactionZone; // Collider that defines the interaction zone
    public Transform playerTransform; // Player's transform

    private bool isCorrectlyAligned = false; // To keep track if this painting is aligned

    void Start()
    {
        StartCoroutine(MoveBookshelfToPosition()); // Test bookshelf movement independently
    }

    void Update()
    {
        var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (rightHandDevice.isValid && interactionZone.bounds.Contains(playerTransform.position))
        {
            if (rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {
                RotatePainting();
            }
        }
    }

    private void RotatePainting()
    {
        if (!isCorrectlyAligned)
        {
            transform.Rotate(0, 0, 90); // Rotate painting by 90 degrees
            // Normalize the Z rotation to the range [0, 360)
            float zRotation = transform.eulerAngles.z % 360;
            if (Mathf.Abs(zRotation) < 1.0f || Mathf.Abs(zRotation - 360) < 1.0f) 
            {
                if (!isCorrectlyAligned)  // Ensure it's only counted once
                {
                    isCorrectlyAligned = true;
                    correctAlignments++;
                    Debug.Log("Painting aligned. Total aligned: " + correctAlignments);
                    if (correctAlignments == 3)
                    {
                        MoveBookshelf();
                    }
                }
            }
        }
    }

    private void MoveBookshelf()
    {
        StartCoroutine(MoveBookshelfToPosition());
    }

    private IEnumerator MoveBookshelfToPosition()
    {
        while (Vector3.Distance(bookshelf.position, targetPosition) > 0.01f)
        {
            bookshelf.position = Vector3.MoveTowards(bookshelf.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("Moving bookshelf to: " + bookshelf.position); // Output current position for debugging
            yield return null;
        }
    }

}
