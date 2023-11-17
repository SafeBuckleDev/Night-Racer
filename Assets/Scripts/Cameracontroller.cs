using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    public GameObject Player;
    private controller PLC;
    public GameObject Child;
    public float speed;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Child = Player.transform.Find("camera.contraint").gameObject;
        PLC = Player.GetComponent<controller>();
    }
    private void FixedUpdate()
    {
        Follow();

        speed = (PLC.KPH >= 50) ? 20 : PLC.KPH / 4;
    }
    private void Follow()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Child.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(Player.gameObject.transform.position);
    }
}
