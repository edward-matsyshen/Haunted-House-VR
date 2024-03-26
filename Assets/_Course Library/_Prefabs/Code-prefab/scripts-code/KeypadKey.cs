using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class KeypadKey : MonoBehaviour
{
    public string key;
    public GameObject displayText;

    public void SendKey()
    {
        this.transform.GetComponentInParent<KeypadController>().PasswordEntry(key);
        Debug.Log($"Sending Key: {this.key}");
    }
}


