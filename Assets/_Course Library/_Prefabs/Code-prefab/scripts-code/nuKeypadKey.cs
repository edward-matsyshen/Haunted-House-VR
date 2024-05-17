
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class nuKeypadKey : MonoBehaviour
{
    // Changed from 'public string key' to allow multiple characters.
    public string keys;
    public GameObject displayText;

    public void SendKeys()
    {
        // Assuming PasswordEntry can handle strings longer than 1 character.
        this.transform.GetComponentInParent<KeypadController>().PasswordEntry(keys);
        Debug.Log($"Sending Keys: {this.keys}");
    }
}
