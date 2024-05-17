using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mutantChase01 : MonoBehaviour
{
    public Transform playerTransform;
    private NavMeshAgent agent;
    public FlashlightManager flashlightManager;
    public float chaseDuration = 5.0f;

    private bool isChasing = false;
    private float chaseTimer = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Initialize the NavMeshAgent component
    }

    void Update()
    {
        if (flashlightManager.state == FlashlightState.On && !isChasing)
        {
            StartChase();
        }
        else if (flashlightManager.state == FlashlightState.Off && isChasing)
        {
            StopChase();
        }

        if (isChasing)
        {
            if (chaseTimer > 0)
            {
                chaseTimer -= Time.deltaTime;
                agent.SetDestination(playerTransform.position);
            }
            else
            {
                StopChase();
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            if (hit.collider.gameObject.name == "Hole")
            {
                StartCoroutine(FallDown());  // Coroutine to handle the falling effect
            }
        }
    }

    IEnumerator FallDown()
    {
        agent.isStopped = true;  // Stop the mutant from moving
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(90, 0, 0);  // Rotate to simulate falling down

        float duration = 1.0f;  // Duration of the fall
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, startPosition - new Vector3(0, 1, 0), elapsed / duration);  // Move down
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);  // Rotate to fall
            yield return null;
        }

        Destroy(gameObject);  // Destroy the mutant after falling
    }

    void StartChase()
    {
        isChasing = true;
        chaseTimer = chaseDuration;
        agent.isStopped = false;
    }

    void StopChase()
    {
        isChasing = false;
        agent.isStopped = true;
    }
}