using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissTrig : MonoBehaviour
{
    public List<GameObject> objectsToDisappear = new List<GameObject>();  // List of objects to disappear

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToDisappear)
            {
                if (obj != null)
                {
                    // Disable the Renderer to make the object invisible
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.enabled = false;
                    }

                    // Disable the Collider to make the object non-interactive
                    Collider collider = obj.GetComponent<Collider>();
                    if (collider != null)
                    {
                        collider.enabled = false;
                    }

                    Debug.Log(obj.name + " has been disabled."); // Optional logging
                }
            }
        }
    }
}