using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NavKeypad
{
    public class KeypadButton : MonoBehaviour
    {
        // Removed unused Value property and corresponding private field
        [Header("Button Animation Settings")]
        [SerializeField] private float bttnspeed = 0.1f; // Speed of the button press animation
        [SerializeField] private float moveDist = 0.0025f; // Distance the button moves when pressed
        [SerializeField] private float buttonPressedTime = 0.1f; // Time the button stays pressed before returning

        [Header("Component References")]
        [SerializeField] private Keypad keypad; // Reference to the keypad this button belongs to

        [SerializeField] public string ButtonValue; // The value this button represents

        [SerializeField] private Renderer buttonRenderer; // Renderer for the button to change its color on press
        [SerializeField] private Color pressColor = Color.gray; // Color of the button when pressed
        private Color originalColor; // Original color of the button

        private bool isCooldown = false; // Flag to prevent spamming the button press
        [SerializeField] private float cooldownDuration = 0.5f; // Duration of the cooldown period


        private void OnTriggerEnter(Collider other)
        {
            // This method is triggered when another collider enters this button's collider
            // It checks if the collider belongs to the player and if the button is not on cooldown
            if (other.CompareTag("PlayerHand") && !isCooldown)
            {
                PressButton(); // Trigger the button press action
                StartCoroutine(Cooldown()); // Start the cooldown coroutine
            }
        }

        private IEnumerator Cooldown()
        {
            isCooldown = true;
            yield return new WaitForSeconds(cooldownDuration);
            isCooldown = false;
        }

        public void PressButton()
        {
            if (!moving) // Check if the button is not already moving
            {
                keypad.AddInput(ButtonValue); // Send this button's value to the keypad
                StartCoroutine(MoveSmooth()); // Start the animation coroutine
            }
        }

        private bool moving; // Flag to indicate if the button is currently moving

        private IEnumerator MoveSmooth()
        {
            // This coroutine animates the button press and release
            buttonRenderer.material.color = pressColor; // Change color to indicate being pressed
            moving = true;

            // Move the button down
            Vector3 startPos = transform.localPosition;
            Vector3 endPos = transform.localPosition + new Vector3(0, 0, moveDist);
            yield return MoveOverTime(startPos, endPos);

            // Wait for a bit
            yield return new WaitForSeconds(buttonPressedTime);

            // Move the button back up
            startPos = transform.localPosition;
            endPos = transform.localPosition - new Vector3(0, 0, moveDist);
            yield return MoveOverTime(startPos, endPos);

            moving = false;
            buttonRenderer.material.color = originalColor; // Reset the button color
        }

        private IEnumerator MoveOverTime(Vector3 startPos, Vector3 endPos)
        {
            float elapsedTime = 0;
            while (elapsedTime < bttnspeed)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / bttnspeed);
                transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }
            transform.localPosition = endPos;
        }
    }
}