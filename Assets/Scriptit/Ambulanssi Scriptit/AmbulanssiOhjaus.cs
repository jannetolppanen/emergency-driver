using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class AmbulanssiOhjaus : MonoBehaviour
{
    [Header("Wheelcolliderit")]
    [SerializeField] WheelCollider FR;
    [SerializeField] WheelCollider FL;
    [SerializeField] WheelCollider BR;
    [SerializeField] WheelCollider BL;

    [Header("WheelMeshit")]
    [SerializeField] Transform FR_transform;
    [SerializeField] Transform FL_transform;
    [SerializeField] Transform BR_transform;
    [SerializeField] Transform BL_transform;

    public float speed = 1000f;
    public float breaks = 400f;
    public float maxTurn = 15f;
    private float speedNow = 0f;
    private float breaksNow = 0f;
    private float turnNow = 0f;


    private void FixedUpdate()
    {
        SpeedandBreak();
        CarTurn();
        UpdateWheels();
    }
    // Kääntyminen
    private void CarTurn()
    {
        turnNow = maxTurn * Input.GetAxis("Horizontal");
        FR.steerAngle = turnNow;
        FL.steerAngle = turnNow;
    }
    // Kaasu ja Jarru
    private void SpeedandBreak()
    {
        // Speed
        speedNow = speed * Input.GetAxis("Vertical");

        // Brake
        if (Input.GetKey(KeyCode.Space))
        {
            breaksNow = breaks;
        }
        else
        {
            breaksNow = 0f;
        }
        Debug.Log("Current Speed: " + speed);

        // Frontwheels motor only
        FR.motorTorque = speedNow;
        FL.motorTorque = speedNow;

        // Brake force to all 4 wheels
        FR.brakeTorque = breaksNow;
        FL.brakeTorque = breaksNow;
        BR.brakeTorque = breaksNow;
        BL.brakeTorque = breaksNow;
    }
    // Pyörien Kääntyminen ajaessa
    private void UpdateWheels()
    {
        UpdateWheel(FR , FR_transform);
        UpdateWheel(FL , FL_transform);
        UpdateWheel(BL , BL_transform);
        UpdateWheel(BR , BR_transform);
    }
    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
