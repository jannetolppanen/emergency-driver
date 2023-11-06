using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class AmbulanssiOhjaus : MonoBehaviour
{
    [Header("Wheelcolliders")]
    [SerializeField] WheelCollider FR;
    [SerializeField] WheelCollider FL;
    [SerializeField] WheelCollider BR;
    [SerializeField] WheelCollider BL;

    public float speed = 1000f;
    public float breaks = 400f;
    public float maxTurn = 15f;
    private float speedNow = 0f;
    private float breaksNow = 0f;
    private float turnNow = 0f;


    private void FixedUpdate()
    {
        // add Speed!
        speedNow = speed * Input.GetAxis("Vertical");

        // add Breaks!
        if(Input.GetKey(KeyCode.Space))
        {
            breaksNow = breaks;
        }
        else
        {
            breaksNow = 0f;
        }

        // Frontwheels motor only
        FR.motorTorque = speedNow;
        FL.motorTorque = speedNow;

        // Brake force to all 4 wheels
        FR.brakeTorque = breaksNow;
        FL.brakeTorque = breaksNow;
        BR.brakeTorque = breaksNow;
        BL.brakeTorque = breaksNow;

        // Turning
        turnNow = maxTurn * Input.GetAxis("Horizontal");
        FR.steerAngle = turnNow;
        FL.steerAngle = turnNow;

    }
}
