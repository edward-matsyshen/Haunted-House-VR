using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("Open"); // Trigger the "Open" animation state
    }

    public void CloseDoor()
    {
        animator.SetTrigger("Close"); // Trigger the "Open" animation state
    }
}

