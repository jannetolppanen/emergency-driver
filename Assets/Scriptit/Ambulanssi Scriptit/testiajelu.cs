using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testiajelu : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] waypoints;
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float obstacleDistance;
    public int currentWaypointIndex = 0;

    void Update()
    {
        MoveToWaypoint();
        tormaysRay();
    }

    void tormaysRay()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        Ray ray = new Ray(rayOrigin, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * obstacleDistance, Color.red);


        if (Physics.Raycast(ray, out hit, obstacleDistance))
        {
            speed = 0f;
        }
        else
        {
            speed = 10f;
        }
    }

    void MoveToWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector3 direction = targetWaypoint.position - transform.position;

            // Move towards the waypoint
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            // Rotate towards the waypoint
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the car is close enough to the waypoint
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.5f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // Reset the current waypoint index to loop back to the start
            currentWaypointIndex = 0;
        }
    }
}
