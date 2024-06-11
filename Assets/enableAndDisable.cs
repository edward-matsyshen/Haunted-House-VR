using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enableAndDisable : MonoBehaviour
{
    public GameObject objectToDisable; // Assign the GameObject you want to check if it's disabled
    public List<GameObject> objectsToEnable; // Assign the list of GameObjects you want to enable
    public string nextNodeName = "NextScene"; // Name of the scene to transition to
    public float delayInSeconds = 3.0f; // Delay before transitioning to the next scene

    void Start()
    {
        // Optionally, call ToggleObjects here or based on some other condition
    }

    void Update()
    {
        // You can place the ToggleObjects call here if you want to trigger it under specific conditions
        if (Input.GetKeyDown(KeyCode.Space)) // Example: Press Space to toggle
        {
            ToggleObjects();
        }
    }

    public void ToggleObjects()
    {
        // Check if objectToDisable is not null and is already disabled
        if (objectToDisable != null && !objectToDisable.activeInHierarchy)
        {
            // Iterate over each GameObject in objectsToEnable and enable them
            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true); // Enable the object
                }
            }
            StartCoroutine(DelayedTransition()); // Start the delayed transition
        }
    }

    IEnumerator DelayedTransition()
    {
        yield return new WaitForSeconds(delayInSeconds); // Wait for the specified delay
        SceneManager.LoadScene(nextNodeName); // Load the next scene
    }
}