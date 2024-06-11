using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostTeleport : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Transform teleportDestination; // Destination to teleport the player
    public float minTeleportTime = 5f; // Minimum time between ghost's teleports
    public float maxTeleportTime = 10f; // Maximum time between ghost's teleports
    public float teleportRange = 10f; // Range within which the ghost can teleport around the player

    private UnityEngine.AI.NavMeshAgent agent; // The NavMesh agent for movement, if needed for other tasks

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(TeleportRandomly());
        // Ensure there is a Collider component marked as a trigger
        EnsureTriggerCollider();
    }

    void Update()
    {
        FacePlayer(); // Continuously face towards the player
    }

    IEnumerator TeleportRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTeleportTime, maxTeleportTime));
            Teleport();
        }
    }

    void Teleport()
    {
        Vector3 randomDirection = Random.insideUnitSphere * teleportRange;
        randomDirection += playerTransform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, teleportRange, 1))
        {
            finalPosition = hit.position;
        }
        else
        {
            finalPosition = transform.position; // Stay in place if no valid point is found
        }

        agent.Warp(finalPosition); // Teleport the ghost to the final position
    }

    void FacePlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0; // Keep the rotation purely on the horizontal plane
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void EnsureTriggerCollider()
    {
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>(); // Add a box collider if none exists
        }
        collider.isTrigger = true; // Set the collider as a trigger
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    void TeleportPlayer(Transform player)
    {
        player.position = teleportDestination.position; // Teleport the player to the destination
        player.rotation = teleportDestination.rotation; // Adjust player's rotation if necessary
    }
}