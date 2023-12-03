using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kolari : MonoBehaviour
{
    AudioSource audiosource;
    public GameObject Ending;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Ending)
        {
            print("asd!!");
            audiosource.Play();

        }
        
    }

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }
}
