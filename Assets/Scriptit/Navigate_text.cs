using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigate_text : MonoBehaviour
{
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0)
        {
            Destroy(gameObject);
        }
    }
}
