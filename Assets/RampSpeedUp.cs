using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSpeedUp : MonoBehaviour
{
   public controller plCon;
    public int torqueforce;
        
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(plCon.KPH<= 20)
        {
            plCon.motortorque= 1000;
        }
    }
}
