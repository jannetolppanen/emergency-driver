using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punainen_auto : MonoBehaviour
{
    public float raycastDistance;
    public testiajelu script;

    void Start()
    {
        script = GetComponent<testiajelu>();
    }
    void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        Ray ray = new Ray(rayOrigin, -transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastDistance) && script.carFront == false)
        {
            if (hit.collider.CompareTag("Player"))
            {
                script.brake();
            }
        }
    }
}
