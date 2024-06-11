using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunkleMove : MonoBehaviour
{
    public Transform targetObject; // Object the ghost moves towards
    public Transform playerTransform; // Player's transform
    private NavMeshAgent agent; // NavMesh agent to enable navigation

    public float disableDistance = 2.0f; // Distance at which the ghost disables the target object
    public float pushForce = 5.0f; // Force used to push the player

    private bool targetDisabled = false; // To check if the target has already been disabled

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveTowardsTarget();

        // Check if the target object is close enough to disable it
        if (Vector3.Distance(transform.position, targetObject.position) <= disableDistance && !targetDisabled)
        {
            DisableTargetObject();
        }
    }

    void MoveTowardsTarget()
    {
        if (!targetDisabled)
        {
            agent.SetDestination(targetObject.position);
        }
    }

    void DisableTargetObject()
    {
        // Example of disabling, here we just deactivate the gameObject
        targetObject.gameObject.SetActive(false);
        targetDisabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PushPlayer(other);
        }
    }

    void PushPlayer(Collider player)
    {
        Vector3 pushDirection = (player.transform.position - transform.position).normalized;
        float pushDistance = 1.0f; // You can adjust the distance based on how far you want the player to move
        float pushSpeed = 5.0f; // Speed of the push effect
        StartCoroutine(MovePlayer(player.transform, pushDirection, pushDistance, pushSpeed));
    }

    IEnumerator MovePlayer(Transform playerTransform, Vector3 direction, float distance, float speed)
    {
        float startTime = Time.time;
        Vector3 startPosition = playerTransform.position;
        Vector3 endPosition = startPosition + direction * distance;

        while (Time.time - startTime < distance / speed)
        {
            playerTransform.position = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime) * speed / distance);
            yield return null;
        }

        playerTransform.position = endPosition;
    }
}