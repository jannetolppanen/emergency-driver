using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moottori : MonoBehaviour
{
    AudioSource audioSource;
    private float targetPitch;
    public float incrementPitch = 0.4f;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
 
 
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W))
        {
            targetPitch = 1.8f;
        }
     
        if(Input.GetKeyUp(KeyCode.UpArrow) | Input.GetKeyUp(KeyCode.W))
        {
            incrementPitch = 0.8f;
            targetPitch = 1f;
        }
       
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, incrementPitch * Time.deltaTime);
     
    }
 
}