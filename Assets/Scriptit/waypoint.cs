using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    public Transform LinkedWaypoint;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, Vector3.one);
        if (LinkedWaypoint != null )
        {
        Gizmos.DrawLine(transform.position, LinkedWaypoint.position);
        }
    }
}
