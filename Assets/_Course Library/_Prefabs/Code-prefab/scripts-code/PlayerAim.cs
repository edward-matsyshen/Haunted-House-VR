using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerAim : MonoBehaviour
{
    public Transform headPos;

    private void Update()
    {
        // Perform a raycast from the head position forward
        if (Physics.Raycast(headPos.position, headPos.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(headPos.position, headPos.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            // Check if the hit object is within 3 meters
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance <= 3f)
            {
                // Get the right hand controller device
                var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                // Check if the A button is pressed
                rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButtonPressed);

                if (aButtonPressed)
                {
                    // Trigger the action based on what was hit
                    if (hit.transform.GetComponent<KeypadKey>() != null)
                    {
                        hit.transform.GetComponent<KeypadKey>().SendKey();
                    }
                    else if (hit.transform.name == "door")
                    {
                        hit.transform.GetComponent<DoorController>().OpenClose();
                    }
                }
            }
        }
    }
}