using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    public GameObject Player;
    private controller PLC;
    public GameObject Child;
    public float speed;
    public float defaultFov=0,desiredFOV=0;
    [Range(0,2)]public float smoothTime= 0;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Child = Player.transform.Find("camera.contraint").gameObject;
        PLC = Player.GetComponent<controller>();
        defaultFov = Camera.main.fieldOfView;
    }
    private void FixedUpdate()
    {
        Follow();
        BoostFov();
        speed = (PLC.KPH >= 50) ? 20 : PLC.KPH / 4;
    }
    private void Follow()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Child.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(Player.gameObject.transform.position);
    }

    private void BoostFov()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, Time.deltaTime * smoothTime);
        }
        else
        {

            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaultFov, Time.deltaTime * smoothTime);
        }
    }
}
