using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kolari : MonoBehaviour
{
    AudioSource audiosource;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "End")
        {
            audiosource.Play();
        }
    }
}
