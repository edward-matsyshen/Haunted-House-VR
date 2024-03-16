using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class KeyPadCon : MonoBehaviour
{
    public List<int> correctPassword = new List<int>();
    private List<int> inputPasswordList = new List<int>();
    [SerializeField] private GameObject textDisplayObject; // Reference to the GameObject with TextMeshProUGUI
    private TextMeshProUGUI textDisplay; // To access the TextMeshProUGUI component
    [SerializeField] private float resetTime;
    [SerializeField] private string successText;
    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPassword;
    public UnityEvent onIncorrectPassword;

    public bool allowMultipleActivations = false;
    private bool hasUsedCorrectCode = false;
    public bool HasUsedCorrectCode { get { return hasUsedCorrectCode; } }

    void Start()
    {
        // Initialize the TextMeshProUGUI component
        if (textDisplayObject != null)
            textDisplay = textDisplayObject.GetComponent<TextMeshProUGUI>();
    }

    public void UserNumberEntry(int selectedNum)
    {
        if (inputPasswordList.Count >= 3)
            return;

        inputPasswordList.Add(selectedNum);
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (textDisplay == null) return; // Ensure the TextMeshProUGUI component is referenced

        textDisplay.text = ""; // Reset text
        foreach (int num in inputPasswordList)
        {
            textDisplay.text += num.ToString(); // Append each number to the display text
        }
    }
}