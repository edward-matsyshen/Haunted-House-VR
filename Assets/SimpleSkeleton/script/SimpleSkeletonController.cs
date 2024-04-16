using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSkeletonController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    Animator animator;
    public float movementSpeed = 3.0f; // NPC's movement speed towards the player

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAnimation();
        if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    void HandleAnimation()
    {
        // Handle animations based on keyboard inputs
        if (Input.GetKey(KeyCode.L))
            animator.SetTrigger("lying");
        else if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("jump");
        else if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("knockdown");
        else if (Input.GetKeyDown(KeyCode.Mouse0))
            animator.SetTrigger("punch_L");
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            animator.SetTrigger("punch_R");

        // Set animation parameters
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.LeftShift))
            animator.SetBool("running", true);
        else
            animator.SetBool("running", false);

        if (Input.GetKey(KeyCode.LeftControl))
            animator.SetBool("sidefix", true);
        else
            animator.SetBool("sidefix", false);
    }

    void MoveTowardsPlayer()
    {
        // Compute direction from NPC to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Move NPC towards the player
        transform.position += directionToPlayer * movementSpeed * Time.deltaTime;

        // Make the NPC face the player
        transform.LookAt(player.position);
    }
}