using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hätävilkku : MonoBehaviour
{
    public float raycastDistance = 15f;
    public GameObject lights;

    void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        Ray ray = new Ray(rayOrigin, -transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                lights.SetActive(true);
            }
        }
    }
}

