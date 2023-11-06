using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    internal enum driveType 
    {
        frontWheelDrive,
        rearWheelDrive,
        fourWheelDrive
    }
    [SerializeField]private driveType drive;


    private InputManager Im;
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    private Rigidbody rb;
    public float KPH;
    public int motortorque = 100;
    public float radius = 6;
    public float steeringMax = 4;
    void Start()
    {
        getObjects();
        
    }


    private void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
        steerVehicle();
    }

    private void moveVehicle()
    {
        float totalPower;

        if(drive== driveType.fourWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = Im.vertical*(motortorque/4);
            }

        }
        else if(drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = Im.vertical * (motortorque / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length-2; i++)
            {
                wheels[i].motorTorque = Im.vertical * (motortorque / 2);
            }
        }

        KPH = rb.velocity.magnitude * 3.6f;
    }
    private void steerVehicle()
    {
        //acerman steering formula
        //steerangle = Mathf.Rad2Deg * Mathf.Atan(2.55f/(radius+(1.5f/2)))* horizontalInput;

        if (Im.horizontal > 0)
        {
            //rear tracks size is set to 1.5f
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius + (1.5 / 2)))) * Im.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius - (1.5 / 2)))) * Im.horizontal;
        }else if (Im.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius - (1.5 / 2)))) * Im.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan((float)(2.55f / (radius + (1.5 / 2)))) * Im.horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }



        //for (int i = 0; i < wheels.Length - 2; i++)
        //{
        //    wheels[i].steerAngle = Im.horizontal * steeringMax;
        //}
    }
    void animateWheels()
    {
        Vector3 wheelposition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for(int i = 0;i<4; i++)
        {
            wheels[i].GetWorldPose(out wheelposition, out wheelRotation);
            wheelMesh[i].transform.position = wheelposition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
             
    }
    private void getObjects()
    {
        Im = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }
    
}
