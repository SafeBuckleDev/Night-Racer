using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool handBrake;
    public bool boosting;


    private void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handBrake = (Input.GetAxis("Jump")!=0)?true:false;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            boosting = true;
        }
        else
        {
            boosting = false;
        }
    }
}
