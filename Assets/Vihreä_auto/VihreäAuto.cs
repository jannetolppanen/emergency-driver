using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VihreäAuto : MonoBehaviour
{
    public float raycastDistance;
    public GameObject lights;
    private testiajelu script;

    void Start()
    {
        script = GetComponent<testiajelu>();
    }
    void Update()
    {
        if(script.PlayerBack)
        {
            lights.SetActive(true);
        }
    }

}

