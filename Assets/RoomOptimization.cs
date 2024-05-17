using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOptimization : MonoBehaviour
{
    public DoorController doorController; // Reference to the DoorController
    public GameObject nextRoomSection; // Assign the next room section that should be activated

    void Start()
    {
        if (nextRoomSection != null)
            nextRoomSection.SetActive(false); // Ensure the next room section is disabled on game start

        if (doorController != null)
            doorController.OnDoorUnlocked += ActivateNextRoomSection; // Subscribe to the OnDoorUnlocked event
    }

    private void OnDestroy()
    {
        if (doorController != null)
            doorController.OnDoorUnlocked -= ActivateNextRoomSection; // Unsubscribe to prevent memory leaks
    }

    private void ActivateNextRoomSection()
    {
        if (nextRoomSection != null)
            nextRoomSection.SetActive(true); // Activate the next room section when the door is unlocked
    }
}
