using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorUp : MonoBehaviour
{
    public GameObject elevator; // Reference to the elevator GameObject
    public bool isRaising = false; // To prevent multiple triggers
    public Animator elevatorAnimator; // Animator component of the elevator
    public string elevatorUpAnimation = "Elevator Up"; // Name of the up animation
    public string elevatorStopAnimation = "New State"; // Name of the stop animation

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRaising)
        {
            StartCoroutine(MoveElevator(other));
        }
    }

    IEnumerator MoveElevator(Collider player)
    {
        isRaising = true;
        elevatorAnimator.Play(elevatorUpAnimation);

        // Parent the player to the elevator to move them with it
        player.transform.SetParent(elevator.transform);

        yield return new WaitForSeconds(0.05f); // Small delay before starting the elevator
        yield return new WaitForSeconds(30.0f); // Duration of the elevator animation

        elevatorAnimator.Play(elevatorStopAnimation);

        // Unparent the player from the elevator
        player.transform.SetParent(null);

        isRaising = false;
    }
}