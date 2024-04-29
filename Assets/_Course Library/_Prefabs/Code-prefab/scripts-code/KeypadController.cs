using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class KeypadController : MonoBehaviour
{
    // Dictionary to map door names to lists of corresponding passwords
    public Dictionary<string, List<string>> doorNameToPasswords = new Dictionary<string, List<string>>();
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

        // Initialize the dictionary with lists of passwords
        // Example: doorNameToPasswords.Add("DoorMesh1", new List<string> { "13" });
        //          doorNameToPasswords.Add("GlassDoorMesh", new List<string> { "179", "197", "719" });

        doorNameToPasswords.Add("DoorMesh1", new List<string> { "13" });

        doorNameToPasswords.Add("DoorMesh2", new List<string> { "284" });

        doorNameToPasswords.Add("GlassDoorMesh", new List<string> { "179", "197", "719", "791", "917", "971" });

        doorNameToPasswords.Add("DoorMesh4", new List<string> { "163", "136", "316", "361", "613", "631" });
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
            if (doorNameToPasswords.ContainsKey(doorName))
            {
                List<string> passwords = doorNameToPasswords[doorName];
                if (passwords.Contains(enteredPassword))
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