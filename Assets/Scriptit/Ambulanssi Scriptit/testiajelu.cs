using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testiajelu : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] waypoints;
    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float frontRayDistance;
    public float backRayDistance;
    public int currentWaypointIndex = 0;
    public bool carFront = false;
    public bool PlayerBack = false;
    public bool greenCar = false;

    void Update()
    {
        MoveToWaypoint();
        tormaysRay();
        takaRay();

        if(!greenCar)
        {
            if(!carFront && !PlayerBack)
            {
                speed = 10f;
            }
            else
            {
                speed = 0f;
            }
        }
        else if(greenCar)
        {
            if(carFront)
            {
                speed = 0;
            }
            else
            {
                speed = 10;
            }
        }
    }

    void tormaysRay()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        Ray ray = new Ray(rayOrigin, transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * frontRayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, frontRayDistance))
        {
            carFront = true;
        }
        else
        {
            carFront = false;
        }
    }
    void takaRay()
    {
        Vector3 rayOrigin1 = transform.position + Vector3.up * 1f;
        Vector3 rayDirection1 = -transform.forward;
        
        Vector3 rayOrigin2 = transform.position + Vector3.up * 1f + Vector3.right * 0.5f;
        Vector3 rayDirection2 = Quaternion.Euler(0, 10, 0) * -transform.forward; // 10 degrees to the right

        Vector3 rayOrigin3 = transform.position + Vector3.up * 1f - Vector3.right * 0.5f;
        Vector3 rayDirection3 = Quaternion.Euler(0, -10, 0) * -transform.forward; // 10 degrees to the left

        if (RaycastAndHandle(rayOrigin1, rayDirection1) ||
        RaycastAndHandle(rayOrigin2, rayDirection2) ||
        RaycastAndHandle(rayOrigin3, rayDirection3))
        {
            PlayerBack = true;
        }
        else
        {
            PlayerBack = false;
        }
    }

    bool RaycastAndHandle(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * backRayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, backRayDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
        
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
