using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keltainen_auto : MonoBehaviour
{
    public float raycastDistance;
    public GameObject brakeLight;
    private testiajelu script;

    void Start()
    {
        script = GetComponent<testiajelu>();
    }
    void Update()
    {
        // Straight back rays
        Vector3 rayOrigin1 = transform.position + Vector3.up * 0.5f;
        Vector3 rayDirection1 = -transform.forward;

        Vector3 rayOrigin2 = transform.position + Vector3.up * 0.5f;
        Vector3 rayDirection2 = Quaternion.Euler(0, 30, 0) * -transform.forward; // 30 degrees to the right

        Vector3 rayOrigin3 = transform.position + Vector3.up * 0.5f;
        Vector3 rayDirection3 = Quaternion.Euler(0, -30, 0) * -transform.forward; // 30 degrees to the left

        RaycastAndHandle(rayOrigin1, rayDirection1);
        RaycastAndHandle(rayOrigin2, rayDirection2);
        RaycastAndHandle(rayOrigin3, rayDirection3);
    }

    void RaycastAndHandle(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                brakeLight.SetActive(true);
                script.brake();
            }
        }
        else
        {
            brakeLight.SetActive(false);
        }
    }
}
