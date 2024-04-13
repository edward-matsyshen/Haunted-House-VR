using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class KeypadController : MonoBehaviour
{
    public DoorController door;
    // Replace the single password string with a list of passwords
    public List<string> validPasswords = new List<string>();
    public int passwordLimit;
    public Text passwordText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private void Start()
    {
        passwordText.text = "";

        // Initialize the validPasswords list here, if needed, or directly in the Unity Editor
        // Example: validPasswords.AddRange(new string[] {"password1", "password2", "password3"});
    }

    public void PasswordEntry(string number)
    {
        if (number == "Clear")
        {
            Clear();
            return;
        }
        else if (number == "Enter")
        {
            Enter();
            return;
        }

        int length = passwordText.text.ToString().Length;
        if (length < passwordLimit)
        {
            passwordText.text += number;
        }
    }

    public void Clear()
    {
        passwordText.text = "";
        passwordText.color = Color.white;
    }

    private void Enter()
    {
        // Check if the entered password matches any in the validPasswords list
        if (validPasswords.Contains(passwordText.text))
        {
            door.lockedByPassword = false;

            if (audioSource != null)
                audioSource.PlayOneShot(correctSound);

            passwordText.color = Color.green;
        }
        else
        {
            if (audioSource != null)
                audioSource.PlayOneShot(wrongSound);

            passwordText.color = Color.red;
        }

        StartCoroutine(waitAndClear());
    }

    IEnumerator waitAndClear()
    {
        yield return new WaitForSeconds(0.75f);
        Clear();
    }
}



