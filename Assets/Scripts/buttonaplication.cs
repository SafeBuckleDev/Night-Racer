using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class buttonaplication : MonoBehaviour
{
    public string url;
   
    public string[] url4;
   [Range(0,2)] public float urlrange;

    public Slider slider;
    public Button button;

    public void openURL()
    {

        Application.OpenURL(url);

    }

    private void Update()
    {
     
       if(urlrange== 1)
       {
            url = url4[1];
       }
       else if(urlrange==0)
       {
            url = url4[0];
       }
       else if (urlrange == 2)
        {
            url = url4[2];
        }

        urlrange = slider.value;
    }
}
