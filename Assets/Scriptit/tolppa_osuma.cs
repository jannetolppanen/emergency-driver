using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tolppa_osuma : MonoBehaviour
{
    public AudioSource Osuma;
    
    void OnCollisionEnter(Collision collision)
    {
        Osuma.Play();
    }

    
}
