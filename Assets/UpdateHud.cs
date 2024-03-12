using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInputUpdater : MonoBehaviour
{
    public TextMeshPro text;

    // Reference to the GameObject containing the variable
    public GameObject rightHandController;

    // Variable to store the text input
    private string userInput;

    void Start()
    {
        text = GetComponent<TextMeshPro>();

        rightHandController = GameObject.Find("RightHand Controller");

        if (rightHandController == null)
        {
            Debug.LogError("Could not find the 'righthand controller' GameObject!");
        }

        text.text = "Some text";
    }

    void Update()
    {
        // Access the variable from the other GameObject
        // Here, we assume that the variable is a public property named 'otherVariable'
        if (rightHandController != null)
        {
            userInput = rightHandController.GetComponent<FlashlightManager>().currentBattery.ToString();
            // Update the TextMeshPro input field with the variable value
            text.text = userInput + "%";
        }
    }
}
