using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptimization : MonoBehaviour
{
    public GameObject[] nextRoomSections; // Assign the next room sections that should be activated
    public GameObject[] previousRoomSections; // Assign the previous room sections that should be deactivated

    void Start()
    {
        // Ensure all next room sections are disabled and all previous room sections are enabled at game start
        if (nextRoomSections.Length > 0)
        {
            foreach (GameObject room in nextRoomSections)
            {
                if (room != null)
                    room.SetActive(false); // Disable each room in nextRoomSections
            }
        }

        if (previousRoomSections.Length > 0)
        {
            foreach (GameObject room in previousRoomSections)
            {
                if (room != null)
                    room.SetActive(true); // Enable each room in previousRoomSections
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player or relevant entity
        if (other.CompareTag("Player"))
        {
            // Activate all next rooms and deactivate all previous rooms when the player enters the trigger zone
            foreach (GameObject room in nextRoomSections)
            {
                if (room != null)
                    room.SetActive(true);
            }

            foreach (GameObject room in previousRoomSections)
            {
                if (room != null)
                    room.SetActive(false);
            }
        }
    }
}