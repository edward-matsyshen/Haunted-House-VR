using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class KeypadRay : MonoBehaviour
{
    public GameObject ScreenText; // Assign in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeypadButton")) // Check if the colliding object is a keypad button
        {
            ScreenText.SetActive(true); // Activate the screen text when colliding with a button
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("KeypadButton")) // Check if the colliding object is a keypad button
        {
            var leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

            if (triggerPressed) // Use the Oculus left-hand trigger button
            {
                // Call a method on the KeypadButton script attached to the button object
                KeypadKey keypadButton = other.GetComponent<KeypadKey>();
                //if (keypadButton != null)
                //{
                //    keypadButton.ActivateButton(); // Assume ActivateButton is the method to be called
                //}
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("KeypadButton")) // Check if the exiting object is a keypad button
        {
            ScreenText.SetActive(false); // Deactivate the screen text when not colliding with a button
        }
    }
}