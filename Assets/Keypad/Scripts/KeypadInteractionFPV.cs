using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace NavKeypad
{
    public class KeypadInteractionFPV : MonoBehaviour
    {
        public Keypad keypad; // Reference to your Keypad script
        public Transform interactionOrigin; // The origin of interaction, e.g., a VR controller or camera
        public LayerMask keypadLayer; // Layer mask to only detect keypad buttons
        public float interactionDistance = 0.5f; // Set this to your preferred interaction distance

        private InputDevice rightHandDevice;

        private void Start()
        {
            // Attempt to get the right hand device at the start
            rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        void Update()
        {
            // Ensure the right hand device is valid
            if (!rightHandDevice.isValid)
            {
                rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            }

            if (rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
            {
                OnAButtonPressed();
            }
        }

        void OnAButtonPressed()
        {
            // Cast a ray from the interaction origin forward
            if (Physics.Raycast(interactionOrigin.position, interactionOrigin.forward, out RaycastHit hit, interactionDistance, keypadLayer))
            {
                // Assuming your keypad buttons have colliders and are differentiated by an identifiable property (e.g., name or tag)
                var button = hit.collider.GetComponent<KeypadButton>(); // Assuming you have a KeypadButton component that can identify the button
                if (button != null)
                {
                    // Call the method to add input on the keypad
                    keypad.AddInput(button.ButtonValue); // Assuming ButtonValue is a property that holds the number or command the button represents
                }
            }
        }
    }
}