using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMove : MonoBehaviour
{
public class ClosestNavMeshPosition : MonoBehaviour
{
    public Transform player;  // Assign the player's transform in the Unity Editor

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player transform not assigned!");
            return;
        }

        // Get the player's position
        Vector3 playerPosition = player.position;

        // Find the closest point on the NavMesh to the player using raycast
        Vector3 closestNavMeshPosition = GetClosestNavMeshPoint(playerPosition);

        // Do something with the closestNavMeshPosition, for example, move an object to that position
        // For demonstration purposes, let's just draw a line in the Scene view to visualize it
        Debug.DrawLine(playerPosition, closestNavMeshPosition, Color.green);
    }

    Vector3 GetClosestNavMeshPoint(Vector3 targetPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(targetPosition, Vector3.down, out hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            // If a valid NavMesh point is found within the raycast, return that point as the closest NavMesh position
            return hit.point;
        }

        // If no valid NavMesh point is found, return the original target position
        return targetPosition;
    }
}
}


