using UnityEngine;
using UnityEngine.XR;

public class KeypadButton01 : MonoBehaviour
{
    private bool isPlayerClose = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand")) // Ensure your hand/controller GameObject has the "PlayerHand" tag
        {
            isPlayerClose = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            isPlayerClose = false;
        }
    }

    void Update()
    {
        // Access the right hand controller
        var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Check if the A button (primaryButton) is pressed
        rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButtonPressed);

        // If the player is close and the A button is pressed, activate the button
        if (isPlayerClose && aButtonPressed)
        {
            ActivateButton();
        }
    }

    void ActivateButton()
    {
        Debug.Log($"Activated and destroyed: {gameObject.name}");
        // Implement what happens when the button is activated
        // This could include triggering a sound, starting an animation, or other game logic.

        // Destroy this GameObject
        Destroy(gameObject);
    }
}