using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PaintingInteraction : MonoBehaviour
{
    public static int correctAlignments = 0;    
    public Transform wall; 
    public Collider interactionZone; 
    public Transform playerTransform;

    public AudioSource audioSource;         
    public AudioClip disappearSound;

    private bool isCorrectlyAligned = false; 

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
        transform.Rotate(0, 0, 90); 
        
        float zRotation = transform.eulerAngles.z % 360;
        if (Mathf.Abs(zRotation) < 1.0f) 
        {
            if (!isCorrectlyAligned)  
            {
                isCorrectlyAligned = true;
                correctAlignments++;
                Debug.Log("Painting aligned. Total aligned: " + correctAlignments);
                if (correctAlignments == 3) 
                {
                    DisappearWall(); 
                }
            }
        }
        else
        {
            if (isCorrectlyAligned) 
            {
                isCorrectlyAligned = false;
                correctAlignments--;
            }
        }
    }

    private void DisappearWall()
    {
        wall.gameObject.SetActive(false);
        Debug.Log("Wall has disappeared.");
        //WIP
        if (audioSource != null && disappearSound != null)
        {
            audioSource.PlayOneShot(disappearSound); 
        }

    }

}
