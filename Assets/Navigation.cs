using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;

    private void OnDrawGizmos()
    {
        if (waypoint1 == null || waypoint2 == null)
        {
            Debug.LogWarning("Please assign both waypoints in the inspector.");
            return;
        }

        Gizmos.color = Color.blue; // You can change the color as needed

        Gizmos.DrawLine(waypoint1.position, waypoint2.position);

        // You can also draw additional gizmos as needed, such as arrows or spheres
        float arrowSize = 0.2f;
        float arrowLength = 0.5f;
        Vector3 direction = (waypoint2.position - waypoint1.position).normalized;
        Vector3 arrowPoint = waypoint1.position + direction * (arrowLength * 0.9f);

        Gizmos.DrawRay(arrowPoint, Quaternion.Euler(0, 0, 135) * direction * arrowSize);
        Gizmos.DrawRay(arrowPoint, Quaternion.Euler(0, 0, -135) * direction * arrowSize);
    }
}
