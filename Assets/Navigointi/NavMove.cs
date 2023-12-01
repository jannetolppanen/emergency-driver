using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMove : MonoBehaviour
{
    public Transform playerTransform;
    public LayerMask navMeshSurfaceLayer;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not assigned!");
        }
    }

    void Update()
    {
        MoveToClosestNavMeshPoint();
    }

    void MoveToClosestNavMeshPoint()
    {
        if (playerTransform != null)
        {
            Vector3 playerPosition = playerTransform.position;

            RaycastHit hit;
            if (Physics.Raycast(playerPosition + Vector3.up * 10f, Vector3.down, out hit, 30f, navMeshSurfaceLayer))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}



