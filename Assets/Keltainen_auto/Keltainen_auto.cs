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
        if(script.PlayerBack)
        {
            brakeLight.SetActive(true);
        }
        else
        {
            brakeLight.SetActive(false);
        }
    }
}
