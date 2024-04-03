using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerAim : MonoBehaviour
{
    public Transform headPos;
    private float lastButtonPressTime = 0f;
    public float buttonPressCooldown = 0.5f; // Seconds

    private void Update()
    {
        // Cooldown check
        if (Time.time - lastButtonPressTime < buttonPressCooldown)
            return;

        // Perform a raycast from the head position forward
        if (Physics.Raycast(headPos.position, headPos.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(headPos.position, headPos.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance <= 3f)
            {
                var rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool aButtonPressed);

                if (aButtonPressed)
                {
                    // Update the last button press time
                    lastButtonPressTime = Time.time;

                    if (hit.transform.GetComponent<KeypadKey>() != null)
                    {
                        hit.transform.GetComponent<KeypadKey>().SendKey();
                    }
                    else if (hit.transform.name == "OpenUp")
                    {
                        hit.transform.GetComponent<DoorController>().OpenUp();
                    }
                }
            }
        }
    }
}
