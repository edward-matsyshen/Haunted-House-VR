using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codeTrig : MonoBehaviour
{
    public DollyInterface controlledScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player GameObject has the tag "Player"
        {
            if (controlledScript != null)
            {
                controlledScript.enabled = true;
                Debug.Log("Controlled Script Enabled!");
            }
        }
    }
}

