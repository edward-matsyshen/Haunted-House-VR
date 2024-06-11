using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class GhostMover : MonoBehaviour
{
    [SerializeField] private AnimancerComponent _Animancer;
    [SerializeField] private ClipTransition _Idle;
    [SerializeField] private ClipTransition _Move;
    [SerializeField] private ClipTransition _TeleportIn;
    [SerializeField] private ClipTransition _TeleportOut;

    private NavMeshAgent _Agent;
    private float _movementInterval = 5.0f; // Time in seconds between moves
    private float _teleportInterval = 10.0f; // Time in seconds between teleports

    private void Awake()
    {
        _Agent = GetComponent<NavMeshAgent>();
        _TeleportOut.Events.OnEnd = StartTeleport;
        StartCoroutine(RandomMovement());
        StartCoroutine(RandomTeleport());
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(_movementInterval);
            MoveToRandomLocation();
        }
    }

    private IEnumerator RandomTeleport()
    {
        while (true)
        {
            yield return new WaitForSeconds(_teleportInterval);
            _Animancer.Play(_TeleportOut);
        }
    }

    private void StartTeleport()
    {
        Vector3 teleportDestination = CalculateTeleportDestination();
        transform.position = teleportDestination;
        _Animancer.Play(_TeleportIn);
    }

    private Vector3 CalculateTeleportDestination()
    {
        // Randomly select a valid NavMesh position within a certain range
        Vector3 randomDirection = Random.insideUnitSphere * 20;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 20, 1);
        return hit.position;
    }

    private void MoveToRandomLocation()
    {
        Vector3 destination = CalculateTeleportDestination(); // Reusing the teleport destination logic
        _Agent.destination = destination;
        _Animancer.Play(_Move);
        _Move.Events.OnEnd = () => _Animancer.Play(_Idle);
    }
}
