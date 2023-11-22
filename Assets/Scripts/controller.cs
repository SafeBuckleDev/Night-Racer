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
    internal enum gearType
    {
        automatic,
        manual
    }
    [SerializeField] private gearType gearChange;
    [SerializeField]private driveType drive;

    public CarManager carManager;
    public GameObject wheelMeshes, wheelColliders;
    public Rigidbody rb; 
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    public float[] slip = new float[4];
    private InputManager Im;
    private GameObject centerOfMass;
    

    [Header("variables")]
    public float totalPower;
    public float maxRPM,minRPM;
    public float wheelsRPM;
    public float smoothTime = 0.1f;
    public float engineRpm;
    public float[]gears;
    public int gearNum;

    public AnimationCurve enginePower;
    public bool AssembleCar;
    public float KPH;
    public int motortorque = 100;
    public float thrust = 1000f;
    public float radius = 6;
    public float addDownForceValue= 50;
    public float Brakepower;
    public float steeringMax = 4;

    void Start()
    {
        getObjects();
        
    }


    private void FixedUpdate()
    {
        addDownForce();
        animateWheels();
        moveVehicle();
        steerVehicle();
        getFriction();
        calculateEnginePower();
        Shifter();
    }
    private void calculateEnginePower()
    {
        wheelRPM();

        totalPower = enginePower.Evaluate(engineRpm) * (gears[gearNum]) * Im.vertical;
        float velocity = 0.0f;
        engineRpm = Mathf.SmoothDamp(engineRpm, 1000 + (Mathf.Abs(wheelsRPM) *3.6f * (gears[gearNum])), ref velocity, smoothTime);
    }
    private void wheelRPM()
    {
        float sum = 0;
        int R = 0;
        for(int i = 0; i< 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
    }
    private void Shifter()
    {
        if(gearChange == gearType.automatic)
        {
            if(engineRpm> maxRPM && gearNum< gears.Length - 1)
            {
                gearNum++;
                carManager.changeGear();
            }
        }
        else
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gearNum >= 0 && gearNum <= 5)
                {
                    gearNum++;
                    carManager.changeGear();
                }
            }
        }
        if(engineRpm< minRPM && gearNum> 0)
        {
            gearNum--;
            carManager.changeGear();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gearNum >= 0 && gearNum < 6)
            {
                gearNum--;
                carManager.changeGear();
            }
        }

    }

    private void moveVehicle()
    {

        if(drive== driveType.fourWheelDrive)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = Im.vertical*(totalPower/4);
            }

        }
        else if(drive == driveType.rearWheelDrive)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = Im.vertical * (totalPower / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length-2; i++)
            {
                wheels[i].motorTorque = Im.vertical * (totalPower / 2);
            }
        }

        KPH = rb.velocity.magnitude * 3.6f;

        if (Im.handBrake)
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = Brakepower;
        }
        else
        {
            wheels[3].brakeTorque = wheels[2].brakeTorque = 0;
        }

        if (Im.boosting)
        {
            rb.AddForce(Vector3.forward * thrust);
            
        }
        
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
        if(AssembleCar== true)
        {
         wheelColliders = GameObject.Find("visuals&colliders");
         wheelMeshes = GameObject.Find("visuals&colliders");

         wheels[0] = wheelColliders.transform.Find("c0").gameObject.GetComponent<WheelCollider>();
         wheels[1] = wheelColliders.transform.Find("c1").gameObject.GetComponent<WheelCollider>();
         wheels[2] = wheelColliders.transform.Find("c2").gameObject.GetComponent<WheelCollider>();
         wheels[3] = wheelColliders.transform.Find("c3").gameObject.GetComponent<WheelCollider>();
       
         wheelMesh[0] = wheelMeshes.transform.Find("0").gameObject;
         wheelMesh[1] = wheelMeshes.transform.Find("1").gameObject;
         wheelMesh[2] = wheelMeshes.transform.Find("2").gameObject;
         wheelMesh[3] = wheelMeshes.transform.Find("3").gameObject;
        }
        centerOfMass = GameObject.Find("centerMass");
        rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void addDownForce()
    {
        rb.AddForce(-transform.up* addDownForceValue*rb.velocity.magnitude);
    }
    private void getFriction()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            WheelHit wheelHit;
            wheels[i].GetGroundHit(out wheelHit);

            slip[i] = wheelHit.forwardSlip;
        }
    }

}
