using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tolppa_osuma : MonoBehaviour
{

AudioSource audioSource;
public AudioClip audio;

void Start () {
    
    audioSource = gameObject.AddComponent<AudioSource>();
}

private void OnCollisionEnter(Collision collision)
{
    
    audioSource.Play();
}
}
