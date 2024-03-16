using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace NavKeypad
{
    public class KeypadInteractionFPV : MonoBehaviour
    {
        private InputDevice rightHandDevice;
        public float interactionDistance = 0.5f; // Set this to your preferred interaction distance

        private void Start()
        {
            // Get the right hand device at the start
            rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        private void Update()
        {
            bool aButtonPressed = false;
            rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out aButtonPressed); // For the A button

            if (aButtonPressed)
            {
                // Perform a proximity check to simulate pressing a button when near enough and A button is pressed
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.TryGetComponent(out KeypadButton keypadButton))
                    {
                        keypadButton.PressButton();
                        break; // Assuming you only want to press one button at a time
                    }
                }
            }
        }
    }
}