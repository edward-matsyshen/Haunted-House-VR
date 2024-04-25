using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class KeypadController : MonoBehaviour
{
    // Dictionary to map door names to their corresponding passwords
    public Dictionary<string, string> doorNameToPassword = new Dictionary<string, string>();
    public List<DoorController> doors; // List of doors
    public int passwordLimit;
    public Text passwordText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private void Start()
    {
        passwordText.text = "";

        // Initialize the dictionary to map door names to their passwords
        // Example: doorNameToPassword.Add("Door1", "password1");
        //          doorNameToPassword.Add("Door2", "password2");
        doorNameToPassword.Add("DoorMesh1", "13");
        doorNameToPassword.Add("DoorMesh2", "101");
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

        int length = passwordText.text.Length;
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
        string enteredPassword = passwordText.text;

        // Check if the entered password matches any value in the dictionary
        bool doorUnlocked = false;
        foreach (var door in doors)
        {
            string doorName = door.gameObject.name;
            if (doorNameToPassword.ContainsKey(doorName) &&
                doorNameToPassword[doorName].Equals(enteredPassword, System.StringComparison.InvariantCultureIgnoreCase))
            {
                door.lockedByPassword = false; // Unlock the door
                door.OpenUp(); // Open the door
                doorUnlocked = true;

                if (audioSource != null)
                    audioSource.PlayOneShot(correctSound);

                passwordText.color = Color.green;
                break;
            }
        }

        if (!doorUnlocked)
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