using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moottori : MonoBehaviour
{
    public AudioSource audioSource;
    private float targetPitch;
    private float incrementPitch;
 

    void Update()
    {
        float verticalInput = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;

        if (verticalInput != 0)
        {
            targetPitch = 1.9f;
        }
        else
        {
            incrementPitch = 0.8f;
            targetPitch = 1f;
        }

        audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, incrementPitch * Time.deltaTime);     
    }
 
}