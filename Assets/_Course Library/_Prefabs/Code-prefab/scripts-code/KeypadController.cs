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

        doorNameToPasswords.Add("DoorMesh1", new List<string> { "888", "13" });

        doorNameToPasswords.Add("DoorMesh2", new List<string> { "888", "284" });

        doorNameToPasswords.Add("TutorailDoorMesh01", new List<string> { "888", "84", "209" });

        doorNameToPasswords.Add("TutorailDoorMesh02", new List<string> { "888", "304" });

        doorNameToPasswords.Add("TutorailDoorMesh03", new List<string> { "888", "532" });

        doorNameToPasswords.Add("TutorailDoorMesh04", new List<string> { "888", "13" });


        doorNameToPasswords.Add("IndoorDoorMesh01", new List<string> { "888", "742", "724", "472", "427", "274", "247" });

        doorNameToPasswords.Add("PuzzleRoomDoorMesh01", new List<string> { "888", "589", "598", "859", "895", "958", "985" });

        //all varations:

        doorNameToPasswords.Add("OutsideDoorMesh", new List<string> { "888", "8367", "8376", "8637", "8673", "8736", "8763", "3867", "3876", "3687", "3678", "3786", "3768", "6837", "6873", "6387", "6378", "6783", "6738", "7836", "7863", "7386", "7368", "7683", "7638" });

        //all varations:

        doorNameToPasswords.Add("DoorMesh4", new List<string> { "888", "163", "136", "316", "361", "613", "631" });
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