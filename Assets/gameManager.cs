using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public controller Kart;
    public GameObject needle;
    private float startPos= 225, endPos= -410;
    private float desiredPos;

    public float vehiclespeed;

    private void Update()
    {
        vehiclespeed = Kart.KPH;
    }
    private void FixedUpdate()
    {
        updateNeedle();
    }
    public void updateNeedle()
    {
        desiredPos = startPos - endPos;
        float temp = vehiclespeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPos - temp * desiredPos));
    }
}
